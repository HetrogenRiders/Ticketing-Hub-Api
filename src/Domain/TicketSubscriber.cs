using System;
using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class TicketSubscriber : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }

        // Whether this subscriber has opted in for filtered notifications
        public bool IsFilteringEnabled { get; set; } = false;

        public virtual Ticket Ticket { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}
