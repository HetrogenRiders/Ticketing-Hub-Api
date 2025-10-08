using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement;

public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
{
    private readonly DBContext _context;

    public DeleteUserValidator(DBContext context)
    {
        _context = context;

        RuleFor(v => v).MustAsync(async (request, cancellation) =>
                await IsSuperAdminExist(request.UserId, cancellation))
            .WithMessage("User can't be deleted as no other Super Admin exists.");
    }


    public async Task<bool> IsSuperAdminExist(Guid userId, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles.ToListAsync(cancellationToken);

        var superAdminRoleId = roles.FirstOrDefault(x => x.RoleName == "SuperAdmin")?.Id;


        var user = await _context.UserRoles
            .Where(x => x.RoleID == superAdminRoleId).ToListAsync(cancellationToken);


        return user != null && user?.Count > 1;
    }
}
