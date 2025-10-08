using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.RolesManagement;

public sealed class GetRoleHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
{
    private readonly DBContext _context;

    public GetRoleHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<GetRoleResponse> Handle(GetRoleRequest request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles
           .Where(x => x.Id == request.RoleId && !x.IsDeleted)
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
                    }).ToList() // Select all RoleIds for each user
                })
               .Select(result => new RoleDto
               {
                   RoleId = result.Role.Id,
                   RoleName = result.Role.RoleName,
                   Description = result.Role.Description,
                   Modules = result.Modules
               })
               .FirstOrDefaultAsync(cancellationToken);

        return new GetRoleResponse
        {
            Roles = roles
        };
    }
}
