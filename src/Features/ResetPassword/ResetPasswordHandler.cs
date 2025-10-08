using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Common.Interfaces;

namespace TicketingHub.Api.Features.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, ResetPasswordResponse>
{
    private readonly DBContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public ResetPasswordHandler(DBContext context, IConfiguration configuration, IEmailService emailService)
    {
        _context = context;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.EmailId == request.Email, cancellationToken);

        if (user == null)
        {
            return new ResetPasswordResponse
            {
                Success = false,
                Message = "User not found."
            };
        }

        var expiryTime = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["ResetPassword:ExpiryTimeInMinutes"]));

        user.ResetPasswordExpiryTime = expiryTime;
        await _context.SaveChangesAsync(cancellationToken);

        var token = EncryptToken(user.UserID, expiryTime);
        var resetLink = $"{_configuration["BaseURL"]}{_configuration["ResetPassword:ResetPasswordPage"]}?token={token}";
        var link = $"<a target=\"_blank\" href={resetLink}>Click here.</a>";

        await _emailService.SendAsync(
            request.Email,
            "Reset Password",
            $"Click the following link to reset your password: {link}");

        return new ResetPasswordResponse
        {
            Success = true,
            Message = "Reset password link sent successfully."
        };
    }

    private string EncryptToken(Guid userId, DateTime expiryTime)
    {
        var token = $"{userId}|{expiryTime}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
    }
}