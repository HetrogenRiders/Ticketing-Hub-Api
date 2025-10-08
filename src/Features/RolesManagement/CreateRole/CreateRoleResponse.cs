namespace TicketingHub.Api.Features.RolesManagement;

public class CreateRoleResponse
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<ModuleConfiguration> Modules { get; set; }
}
