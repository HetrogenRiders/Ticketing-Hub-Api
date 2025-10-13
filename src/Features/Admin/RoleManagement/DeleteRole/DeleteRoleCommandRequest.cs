using MediatR;

namespace TicketingHub.Api.Features.Admin.RoleManagement.DeleteRole
{
    public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
    {
        public Guid RoleId { get; set; }
    }
}
