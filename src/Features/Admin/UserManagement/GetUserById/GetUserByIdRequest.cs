using MediatR;

namespace TicketingHub.Api.Features.Admin.UserManagement.GetUserById
{
    public class GetUserByIdRequest : IRequest<GetUserByIdResponse>
    {
        public Guid UserId { get; set; }
    }
}
