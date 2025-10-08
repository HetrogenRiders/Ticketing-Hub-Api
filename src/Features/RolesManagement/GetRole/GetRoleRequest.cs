using MediatR;

namespace TicketingHub.Api.Features.RolesManagement;

public class GetRoleRequest : IRequest<GetRoleResponse>
{
    public Guid RoleId { get; set; }
}
