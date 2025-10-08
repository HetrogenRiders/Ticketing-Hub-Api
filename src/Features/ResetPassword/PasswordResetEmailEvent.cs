using MediatR;

namespace TicketingHub.Api.Features.ResetPassword;

public class PasswordResetEmailEvent : INotification
{
    public string Email { get; }
    public string ResetLink { get; }

    public PasswordResetEmailEvent(string email, string resetLink)
    {
        Email = email;
        ResetLink = resetLink;
    }
}
