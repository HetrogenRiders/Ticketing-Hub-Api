using MediatR;

namespace TicketingHub.Api.Features.ChangePassword;

public class ChangePasswordRequest : IRequest<ChangePasswordResponse>
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
}
