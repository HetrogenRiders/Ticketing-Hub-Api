using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;
using System.Text;
using System.Security.Cryptography;

namespace TicketingHub.Api.Features.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
{
    private readonly DBContext _context;

    public ChangePasswordHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        // Decode the token
        var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(request.Token));
        var tokenParts = decodedToken.Split('|');
        var userId = Guid.Parse(tokenParts[0]);
        var expiryTime = DateTime.Parse(tokenParts[1]);

        var user = await _context.User.FirstOrDefaultAsync(u => u.UserID == userId, cancellationToken);
        if (user == null || user.ResetPasswordExpiryTime != expiryTime)
        {
            return new ChangePasswordResponse
            {
                Success = false,
                Message = "Invalid or expired reset password link."
            };
        }

        if (DateTime.UtcNow > expiryTime)
        {
            // Expired link
            user.ResetPasswordExpiryTime = null;
            await _context.SaveChangesAsync(cancellationToken);

            return new ChangePasswordResponse
            {
                Success = false,
                Message = "Reset password link has expired."
            };
        }

        // Update user's password and clear the expiry time
        user.Password = HashPassword(request.NewPassword);
        user.ResetPasswordExpiryTime = null;
        await _context.SaveChangesAsync(cancellationToken);

        return new ChangePasswordResponse
        {
            Success = true,
            Message = "Password changed successfully."
        };
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
