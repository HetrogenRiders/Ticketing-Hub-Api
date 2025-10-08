using MediatR;

namespace TicketingHub.Api.Features.Authentication;

public class AuthenticateUserRequest : IRequest<AuthenticateUserResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}