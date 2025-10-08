using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain;

public class Role : AuditableEntity
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
}