using MediatR;

namespace TicketingHub.Api.Features.RolesManagement;

public class EditRoleRequest : IRequest<EditRoleResponse>
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public IEnumerable<ModuleConfiguration> Modules { get; set; }
}