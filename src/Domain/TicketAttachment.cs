using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class TicketAttachment : AuditableEntity
    {
        public Guid Id { get; set; } // AttachmentId
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public Guid UploadedById { get; set; }
        public User UploadedBy { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
    }
}
