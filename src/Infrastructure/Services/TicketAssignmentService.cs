using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Infrastructure.Services
{
    public class TicketAssignmentService : ITicketAssignmentService
    {
        private readonly DBContext _context;
        private readonly ILogger<TicketAssignmentService> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public TicketAssignmentService(DBContext context, ILogger<TicketAssignmentService> logger, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task AssignToDepartmentHeadAsync(Guid ticketId, Guid departmentId)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department?.HeadUserId == null)
            {
                _logger.LogWarning("Department {DepartmentId} has no head assigned for auto-assignment.", departmentId);
                return;
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null) return;

            ticket.AssignedToId = department.HeadUserId.Value;
            ticket.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Ticket {TicketId} auto-assigned to department head {HeadId}.", ticketId, department.HeadUserId.Value);
        }
    }
}
