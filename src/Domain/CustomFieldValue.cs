using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class CustomFieldValue : AuditableEntity
    {
        public Guid Id { get; set; } // FieldValueId
        public Guid CustomFieldId { get; set; }
        public CustomField CustomField { get; set; } = null!;
        public Guid EntityId { get; set; } // could be TicketId or ProjectId
        public string FieldValue { get; set; } = string.Empty;
    }
}
