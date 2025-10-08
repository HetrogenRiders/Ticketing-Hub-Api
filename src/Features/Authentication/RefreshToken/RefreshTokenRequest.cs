using MediatR;

namespace TicketingHub.Api.Features.Authentication;

public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
