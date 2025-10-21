using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.UpdateTicket
{
    public class UpdateTicketHandler : IRequestHandler<UpdateTicketRequest, UpdateTicketResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateTicketHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateTicketResponse> Handle(UpdateTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == request.TicketId, cancellationToken);

            if (ticket == null)
                throw new KeyNotFoundException(_localizer["Ticket not found."]);

            // Update mutable fields
            ticket.Title = request.Title.Trim();
            ticket.Description = request.Description?.Trim();
            ticket.AssignedToId = request.AssignedToId;
            ticket.ProjectId = request.ProjectId;
            ticket.CategoryId = request.CategoryId;
            ticket.SubCategoryId = request.SubCategoryId;
            ticket.PriorityId = request.PriorityId;
            ticket.StatusId = request.StatusId;
            ticket.DueDate = request.DueDate ?? ticket.DueDate;
            ticket.LastModified = DateTime.UtcNow;

            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            // Create a history record for audit
            var history = new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticket.Id,
                ActionType = "Updated",
                OldValue = null,
                NewValue = null,
                ChangedById = request.UpdatedById,
                LastModified = DateTime.UtcNow
            };
            _context.TicketHistories.Add(history);
            await _context.SaveChangesAsync(cancellationToken);

            await tx.CommitAsync(cancellationToken);

            return new UpdateTicketResponse
            {
                Message = _localizer["Ticket updated successfully."]
            };
        }
    }
}
