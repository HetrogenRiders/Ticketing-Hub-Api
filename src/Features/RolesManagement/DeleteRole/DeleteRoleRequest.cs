namespace TicketingHub.Api.Features.RolesManagement;
using MediatR;

public class DeleteRoleRequest : IRequest<DeleteRoleResponse>
{
    public Guid RoleId { get; set; }
}