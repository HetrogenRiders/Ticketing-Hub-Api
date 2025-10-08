using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement;

public class EditUserValidator : AbstractValidator<EditUserRequest>
{
    private readonly DBContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EditUserValidator(DBContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;

        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.EmailId).NotEmpty();

        RuleFor(v => v).MustAsync(async (request, cancellation) =>
                   await CheckForEditUserRole(request.UserId, cancellation))
               .WithMessage("You can't delete user with higher role.");
    }


    public async Task<bool> CheckForEditUserRole(Guid userId, CancellationToken cancellationToken)
    {
        var loggedInUserRoles = _currentUserService.Roles;

        if (loggedInUserRoles.Contains("SuperAdmin"))
        {
            return true;
        }

        var userRoles = await _context.UserRoles
           .Where(x => x.UserID == userId)
           .Join(_context.Roles,
            userRole => userRole.RoleID,
            role => role.Id,
            (userRole, role) => role.RoleName)
            .ToListAsync(cancellationToken); // Get roles of user being updated

        if (userRoles.Contains("SuperAdmin"))
        {
            return false;
        }

        return true;
    }
}