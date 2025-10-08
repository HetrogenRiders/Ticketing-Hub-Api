using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private readonly DBContext _context;

    public CreateUserValidator(DBContext context)
    {
        _context = context;

        RuleFor(v => v.FirstName).NotEmpty();
        RuleFor(v => v.LastName).NotEmpty();
        RuleFor(v => v.EmailId).NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email is required");
        RuleFor(v => v.RoleId).NotEmpty();
        RuleFor(v => v.Password).NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (e.g., @, #, $, etc.).");
        RuleFor(v => v).MustAsync(async (request, cancellation) =>
                await IsEmailExist(request.EmailId, cancellation))
            .WithMessage("Email address already exists.");
    }


    public async Task<bool> IsEmailExist(string emailId, CancellationToken cancellationToken)
    {
        var user = await _context.User
            .FirstOrDefaultAsync(u => u.EmailId == emailId, cancellationToken);


        return user == null;
    }
}
