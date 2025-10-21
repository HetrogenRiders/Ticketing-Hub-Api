using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.Ticketing.Events;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.CreateTicket
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketRequest, CreateTicketResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ISlaCalculationService _slaService;

        public CreateTicketHandler(DBContext context, IStringLocalizer<SharedResource> localizer, ISlaCalculationService slaService)
        {
            _context = context;
            _localizer = localizer;
            _slaService = slaService;
        }

        public async Task<CreateTicketResponse> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            // Build domain entity
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketNumber = $"TCK-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                CreatedById = request.CreatedById,
                AssignedToId = request.AssignedToId,
                DepartmentId = request.DepartmentId,
                ProjectId = request.ProjectId,
                CategoryId = request.CategoryId,
                SubCategoryId = request.SubCategoryId,
                PriorityId = request.PriorityId,
                StatusId = request.StatusId,
                SLAId = request.SLAId,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                IsDeleted = false
            };

            // Compute SLA-based DueDate if SLA provided and no manual override
            if (request.SLAId.HasValue && !request.DueDate.HasValue)
            {
                var sla = await _context.SLAs.AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == request.SLAId.Value, cancellationToken);

                if (sla != null)
                {
                    ticket.DueDate = _slaService.CalculateDueDate(ticket.Created, sla.ResponseTimeInHours, sla.ResolutionTimeInHours);
                }
            }
            else if (request.DueDate.HasValue)
            {
                ticket.DueDate = request.DueDate.Value;
            }

            // Transaction: create ticket + history atomically
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            // TicketHistory: ChangedById is a GUID FK in domain
            _context.TicketHistories.Add(new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticket.Id,
                ActionType = "Created",
                OldValue = null,
                NewValue = null,
                ChangedById = request.CreatedById,
                Created = DateTime.UtcNow
            });

            await _context.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);

            // 🔔 Publish domain event
            //await _mediator.Publish(new TicketCreatedEvent(ticket.Id, ticket.DepartmentId, ticket.CreatedById), cancellationToken);

            return new CreateTicketResponse
            {
                TicketId = ticket.Id,
                Message = _localizer["Ticket created successfully."]
            };
        }
    }
}
