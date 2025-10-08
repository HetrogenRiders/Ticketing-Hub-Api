using MediatR;

namespace TicketingHub.Api.Features.RolesManagement;

public class CreateRoleRequest : IRequest<CreateRoleResponse>
{
    public string RoleName { get; set; }
    public string Description { get; set; }
    public IEnumerable<ModuleConfiguration> Modules { get; set; }
}

public class ModuleConfiguration
{
    public Guid ModuleId { get; set; }
    public string ModuleName { get; set; }
    public bool? CanView { get; set; }

    public bool? CanAdd { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }
}
