using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.ModuleManagement;

public sealed class GetUserModulesByUserIDHandler : IRequestHandler<GetUserModulesByUserIDRequest, GetUserModulesByUserIDResponse>
{
    private readonly DBContext _context;

    public GetUserModulesByUserIDHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<GetUserModulesByUserIDResponse> Handle(GetUserModulesByUserIDRequest request, CancellationToken cancellationToken)
    {
        var userRoles = await GetUserRoleAsync(request.UserId, cancellationToken);
        var modules = await GetUserRoleModules(userRoles, cancellationToken);

        return new GetUserModulesByUserIDResponse
        {
            UserID = request.UserId,
            Modules = modules
        };
    }
    private async Task<List<string>> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.UserRoles
             .Where(ur => ur.UserID == userId)
             .Join(_context.Roles,
                   ur => ur.RoleID,
                   r => r.Id,
                   (ur, r) => r.RoleName)
             .ToListAsync(cancellationToken);
    }
    private async Task<List<UserModule>> GetUserRoleModules(List<string> userRoles, CancellationToken cancellationToken)
    {
        // Preload modules into a dictionary to minimize database calls
        var modules = await _context.Modules
            .ToDictionaryAsync(x => x.Id, x => x.ModuleName, cancellationToken);

        // Fetch only the required module configurations
        var roleModules = await _context.Roles
            .Where(role => userRoles.Contains(role.RoleName) && !role.IsDeleted)
            .SelectMany(role => _context.RoleModuleConfiguration
                .Where(config => config.RoleID == role.Id)
                .Select(config => new UserModule
                {
                    ModuleId = config.ModuleID,
                    ModuleName = modules.ContainsKey(config.ModuleID) ? modules[config.ModuleID] : null,
                    CanView = config.CanView,
                    CanAdd = config.CanAdd,
                    CanEdit = config.CanEdit,
                    CanDelete = config.CanDelete,
                }))
            .ToListAsync(cancellationToken);

        var moduleConfigs = roleModules
             .GroupBy(config => config.ModuleId) // Group by ModuleID to combine entries
             .Select(group => new UserModule
             {
                 ModuleId = group.Key,
                 ModuleName = group.First().ModuleName, // All items in the group share the same ModuleName
                 CanView = group.Any(x => (x.CanView ?? false) || (x.CanAdd ?? false) || (x.CanEdit ?? false) || (x.CanDelete ?? false)), // Handle nullable booleans
                 CanAdd = group.Any(x => x.CanAdd ?? false),
                 CanEdit = group.Any(x => x.CanEdit ?? false),
                 CanDelete = group.Any(x => x.CanDelete ?? false),
             })
             .ToList();

        return moduleConfigs;
    }
}
