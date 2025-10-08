using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Authentication;

public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserRequest>
{
    private readonly DBContext _context;

    public AuthenticateUserValidator(DBContext context)
    {
        _context = context;

        RuleFor(v => v.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
            .MustAsync(UserExists).WithMessage("User not found.");

        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (e.g., @, #, $, etc.).");

        RuleFor(v => v)
            .MustAsync(async (request, cancellation) =>
                await ValidateUserAync(request.Username, request.Password, cancellation))
            .WithMessage("Invalid credentials.");
    }

    private async Task<bool> UserExists(string username, CancellationToken cancellationToken)
    {
        return await _context.User.AnyAsync(u => u.EmailId == username, cancellationToken);
    }

    public async Task<bool> ValidateUserAync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _context.User
            .FirstOrDefaultAsync(u => u.EmailId == username, cancellationToken);

        if (user == null)
        {
            return false;
        }

        return VerifyPassword(password, user.Password);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        // Hash the incoming password using SHA256
        using (var sha256 = SHA256.Create())
        {
            var hashedInputPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedInputPasswordString = Convert.ToBase64String(hashedInputPassword);

            // Compare the hashed input password with the stored hash
            return hashedInputPasswordString == storedHash;
        }
    }
}
