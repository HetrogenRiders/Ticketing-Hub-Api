using MediatR;

namespace TicketingHub.Api.Features.Admin.UserManagement.DeleteUser
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        public Guid UserId { get; set; }
    }
}
