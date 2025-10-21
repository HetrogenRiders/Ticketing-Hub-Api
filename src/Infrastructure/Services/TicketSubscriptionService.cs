using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Infrastructure.Services
{
    public class TicketSubscriptionService : ITicketSubscriptionService
    {
        private readonly DBContext _context;
        private readonly ILogger<TicketSubscriptionService> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public TicketSubscriptionService(DBContext context, ILogger<TicketSubscriptionService> logger, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task AddDefaultSubscribersAsync(Guid ticketId, Guid departmentId, Guid createdById)
        {
            // Fetch users to subscribe (example: department head + department users)
            var department = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null)
            {
                _logger.LogWarning("Department {DepartmentId} not found while subscribing ticket {TicketId}", departmentId, ticketId);
                return;
            }

            // Determine candidate user IDs
            var candidateUserIds = await _context.Users
                .AsNoTracking()
                .Where(u => u.DepartmentId == department.Id && u.IsActive)
                .Select(u => u.Id)
                .ToListAsync();

            // include creator
            candidateUserIds.Add(createdById);

            // include department head if configured
            if (department.HeadUserId.HasValue)
                candidateUserIds.Add(department.HeadUserId.Value);

            var distinct = candidateUserIds.Distinct().ToList();

            // Load existing subscriber user ids for this ticket
            var existing = await _context.TicketSubscribers
                .Where(ts => ts.TicketId == ticketId)
                .Select(ts => ts.UserId)
                .ToListAsync();

            var toAdd = distinct.Except(existing).ToList();

            if (!toAdd.Any()) return;

            foreach (var userId in toAdd)
            {
                _context.TicketSubscribers.Add(new TicketSubscriber
                {
                    Id = Guid.NewGuid(),
                    TicketId = ticketId,
                    UserId = userId,
                    IsFilteringEnabled = false, // default. You can change logic
                    Created = DateTime.UtcNow,
                    CreatedBy = createdById.ToString()
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
