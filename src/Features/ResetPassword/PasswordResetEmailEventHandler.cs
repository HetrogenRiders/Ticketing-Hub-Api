using TicketingHub.Api.Common.Interfaces;
using MediatR;

namespace TicketingHub.Api.Features.ResetPassword;
public class PasswordResetEmailEventHandler : INotificationHandler<PasswordResetEmailEvent>
{
    private readonly IEmailService _emailService;

    public PasswordResetEmailEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(PasswordResetEmailEvent notification, CancellationToken cancellationToken)
    {
        await _emailService.SendAsync(
            notification.Email,
            "Reset Password",
            $"Click the following link to reset your password: {notification.ResetLink}");
    }
}
