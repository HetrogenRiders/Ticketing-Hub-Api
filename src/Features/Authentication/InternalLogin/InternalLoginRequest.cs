using MediatR;

namespace TicketingHub.Api.Features.Authentication.InternalLogin
{
    public class InternalLoginRequest : IRequest<InternalLoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
