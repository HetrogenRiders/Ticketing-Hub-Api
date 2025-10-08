using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement
{
    public sealed class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
    {
        private readonly DBContext _context;

        public GetAllUsersHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _context.User
                .Where(r =>
                string.IsNullOrEmpty(request.SearchKey) ||
                (r.FirstName != null && r.FirstName.Contains(request.SearchKey)) ||
                (r.LastName != null && r.LastName.Contains(request.SearchKey)) ||
                (r.EmailId != null && r.EmailId.Contains(request.SearchKey)))
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
                .Select(result => new UserDto
                {
                    UserId = result.User.UserID,
                    FirstName = result.User.FirstName,
                    LastName = result.User.LastName,
                    EmailId = result.User.EmailId,
                    IsDeleted = result.User.IsDeleted,
                    Roles = result.Roles // Assign the list of RoleIds
                })
                .ToListAsync(cancellationToken);

            // apply sort
            users = ApplySorting(users, request.SortColumn, request.SortDirection);
            // apply pagination
            users = ApplyPagination(users, request.PageNumber, request.PageSize);


            return new GetAllUsersResponse
            {
                Users = users
            };
        }

        public List<UserDto> ApplySorting(List<UserDto> users, string sortColumn, string sortDirection)
        {
            if (users != null && users.Count > 0)
            {
                // Apply sorting based on the provided column and direction
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
                {
                    // Convert the sort direction to lowercase to handle case-insensitivity
                    var isDescending = sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase);
                    // Sorting logic based on the column name
                    switch (sortColumn.ToLower())
                    {
                        case "firstname":
                            users = isDescending ? users.OrderByDescending(r => r.FirstName).ToList() : users.OrderBy(r => r.FirstName).ToList();
                            break;
                        case "lastname":
                            users = isDescending ? users.OrderByDescending(r => r.LastName).ToList() : users.OrderBy(r => r.LastName).ToList();
                            break;
                        case "emailid":
                            users = isDescending ? users.OrderByDescending(r => r.EmailId).ToList() : users.OrderBy(r => r.EmailId).ToList();
                            break;
                        default:
                            users = users.OrderBy(r => r.FirstName)?.ToList(); // Default sort by RoleName
                            break;
                    }
                }
            }
            return users;
        }
        public List<UserDto> ApplyPagination(List<UserDto> users, int pageNumber, int pageSize)
        {
            if (users != null && users.Count > 0)
            {
                users = users.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            return users;
        }
    }
}
