using MediatR;
using System;

namespace TicketingHub.Api.Features.Ticketing.Events
{
    // Simple immutable domain event
    public class TicketCreatedEvent : INotification
    {
        public Guid TicketId { get; }
        public Guid DepartmentId { get; }
        public Guid CreatedById { get; }

        public TicketCreatedEvent(Guid ticketId, Guid departmentId, Guid createdById)
        {
            TicketId = ticketId;
            DepartmentId = departmentId;
            CreatedById = createdById;
        }
    }
}
