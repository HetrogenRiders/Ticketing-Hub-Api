using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class CustomField : AuditableEntity
    {
        public Guid Id { get; set; } // CustomFieldId
        public string EntityType { get; set; } = string.Empty; // e.g., "Ticket"
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty; // Text, Dropdown, Date
        public bool IsRequired { get; set; } = false;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<CustomFieldValue> FieldValues { get; set; } = new List<CustomFieldValue>();
    }
}
