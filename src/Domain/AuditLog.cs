using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class AuditLog : AuditableEntity
    {
        public Guid Id { get; set; } // LogId
        public string EntityName { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = string.Empty; // Created, Updated, Deleted
        public Guid? PerformedById { get; set; }
        public User? PerformedBy { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
