using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.RolesManagement
{
    public sealed class GetAllRolesHandler : IRequestHandler<GetAllRolesRequest, GetAllRolesResponse>
    {
        private readonly DBContext _context;

        public GetAllRolesHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<GetAllRolesResponse> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles
            .Where(role =>
            string.IsNullOrEmpty(request.SearchKey) ||
            (role.RoleName != null && role.RoleName.Contains(request.SearchKey)) ||
            (role.Description != null && role.Description.Contains(request.SearchKey))
            )
            .GroupJoin(_context.RoleModuleConfiguration,
               role => role.Id,
               config => config.RoleID,
               (role, config) => new
               {
                   Role = role,
                   Modules = config.Select(module => new ModuleConfiguration
                   {
                       ModuleId = module.ModuleID,
                       ModuleName = _context.Modules.FirstOrDefault(x => x.Id == module.ModuleID).ModuleName,
                       CanView = module.CanView,
                       CanAdd = module.CanAdd,
                       CanEdit = module.CanEdit,
                       CanDelete = module.CanDelete,
                   }).ToList()
               })
              .Select(result => new RoleDto
              {
                  RoleId = result.Role.Id,
                  RoleName = result.Role.RoleName,
                  Description = result.Role.Description,
                  IsDeleted = result.Role.IsDeleted,
                  Modules = result.Modules
              })
              .ToListAsync(cancellationToken);

            // apply sort
            roles = ApplySorting(roles, request.SortColumn, request.SortDirection);

            // apply pagination
            roles = ApplyPagination(roles, request.PageNumber, request.PageSize);

            return new GetAllRolesResponse
            {
                Roles = roles
            };
        }

        public List<RoleDto> ApplySorting(List<RoleDto> roles, string sortColumn, string sortDirection)
        {
            if (roles != null && roles.Count > 0)
            {
                // Apply sorting based on the provided column and direction
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
                {
                    // Convert the sort direction to lowercase to handle case-insensitivity
                    var isDescending = sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase);

                    // Sorting logic based on the column name
                    switch (sortColumn.ToLower())
                    {
                        case "rolename":
                            roles = isDescending ? roles.OrderByDescending(r => r.RoleName).ToList() : roles.OrderBy(r => r.RoleName).ToList();
                            break;
                        case "description":
                            roles = isDescending ? roles.OrderByDescending(r => r.Description).ToList() : roles.OrderBy(r => r.Description).ToList();
                            break;

                        default:
                            roles = roles.OrderBy(r => r.RoleName)?.ToList(); // Default sort by RoleName
                            break;
                    }
                }
            }
            return roles;
        }

        public List<RoleDto> ApplyPagination(List<RoleDto> roles, int pageNumber, int pageSize)
        {
            if (roles != null && roles.Count > 0)
            {
                roles = roles.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            return roles;
        }
    }
}
