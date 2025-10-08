using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.RolesManagement;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    private readonly DBContext _context;
    public CreateRoleValidator(DBContext context)
    {
        _context = context;

        RuleFor(v => v.RoleName).NotEmpty();

        RuleFor(v => v)
            .MustAsync(async (request, cancellation) =>
                await ValidateRoleAync(request.RoleName, cancellation))
            .WithMessage("Role already exist.");
    }

    public async Task<bool> ValidateRoleAync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(u => u.RoleName == roleName, cancellationToken);

        return role == null;
    }
}