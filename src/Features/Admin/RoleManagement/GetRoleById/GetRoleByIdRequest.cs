using MediatR;

namespace TicketingHub.Api.Features.Admin.RoleManagement.GetRoleById
{
    public class GetRoleByIdRequest : IRequest<GetRoleByIdResponse>
    {
        public Guid RoleId { get; set; }
    }
}
