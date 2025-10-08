using MediatR;

namespace TicketingHub.Api.Features.ResetPassword;

public class ResetPasswordResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}