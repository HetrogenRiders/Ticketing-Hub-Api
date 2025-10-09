using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Notification : AuditableEntity
    {
        public Guid Id { get; set; } // NotificationId
        public Guid? TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public Guid RecipientId { get; set; }
        public User Recipient { get; set; } = null!;
        public string Message { get; set; } = string.Empty;
        public string NotificationType { get; set; } = string.Empty; // Info, Alert, SLA
        public bool IsRead { get; set; } = false;
    }
}
