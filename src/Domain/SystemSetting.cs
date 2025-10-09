using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class SystemSetting : AuditableEntity
    {
        public Guid Id { get; set; } // SettingId
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
