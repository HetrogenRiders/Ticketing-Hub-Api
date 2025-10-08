namespace TicketingHub.Api.Features.RolesManagement;

public class EditRoleResponse
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<ModuleConfiguration> ModuleConfiguration { get; set; }
}