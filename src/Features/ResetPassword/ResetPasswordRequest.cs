using MediatR;

namespace TicketingHub.Api.Features.ResetPassword;

public class ResetPasswordRequest : IRequest<ResetPasswordResponse>
{
    public string Email { get; set; }
}