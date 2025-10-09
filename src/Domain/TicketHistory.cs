using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class TicketHistory : AuditableEntity
    {
        public Guid Id { get; set; } // HistoryId
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public string ActionType { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public Guid? ChangedById { get; set; }
        public User? ChangedBy { get; set; }
    }
}
