using MediatR;

namespace TicketingHub.Api.Features.Admin.RoleManagement.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
    {
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
