using MediatR;

namespace TicketingHub.Api.Features.UserManagement;

public class GetUserRequest : IRequest<GetUserResponse>
{
    public Guid UserId { get; set; }
}
