using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class TicketComment : AuditableEntity
    {
        public Guid Id { get; set; } // CommentId
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public Guid CommentedById { get; set; }
        public User CommentedBy { get; set; } = null!;
        public string CommentText { get; set; } = string.Empty;
    }
}
