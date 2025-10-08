using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement;

public sealed class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly DBContext _context;

    public GetUserHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.User
           .Where(x => x.UserID == request.UserId && !x.IsDeleted)
            .GroupJoin(_context.UserRoles,  // GroupJoin to include all roles per user
                    user => user.UserID,         // The UserID in the User table
                    userRole => userRole.UserID, // The UserId in the UserRoles table
                    (user, userRoles) => new     // Creating the result object
                    {
                        User = user,
                        Roles = userRoles.Select(userRole => new
                        {
                            // Subquery: joining Roles table to get RoleName
                            Role = _context.Roles
                                .Where(role => role.Id == userRole.RoleID)
                                .Select(role => new { role.Id, role.RoleName })
                                .FirstOrDefault() // Get the first matching role, or null if none found
                                 })
                                 .Where(role => role.Role != null)  // Filter out null results if no role was found
                                 .Select(role => new Roles
                                 {
                                     RoleId = role.Role.Id,
                                     RoleName = role.Role.RoleName
                                 })
                                 .ToList() // Convert to a list of roles
                    })
                .Select(result => new UserResponse
                {
                    UserID = result.User.UserID,
                    FirstName = result.User.FirstName,
                    LastName = result.User.LastName,
                    EmailId = result.User.EmailId,
                    Roles = result.Roles // Assign the list of RoleIds
                })
                .FirstOrDefaultAsync(cancellationToken);

        return new GetUserResponse
        {
            User = user
        };
    }
}
