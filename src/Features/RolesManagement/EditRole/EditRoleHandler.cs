using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.RolesManagement;

public sealed class EditRoleHandler(DBContext context) : IRequestHandler<EditRoleRequest, EditRoleResponse>
{
    private readonly DBContext _context = context;

    public async Task<EditRoleResponse> Handle(EditRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FindAsync(request.RoleId);

        if (role == null)
        {
            throw new Exception("Role not found");
        }

        if (request.RoleName != null)
            role.RoleName = request.RoleName;
        if (request.Description != null)
            role.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        // update module configuration
        foreach (var item in request.Modules)
        {
            var moduleConfiguration = await _context.RoleModuleConfiguration.
                FirstOrDefaultAsync(x => x.ModuleID == item.ModuleId && x.RoleID == role.Id, cancellationToken);


            // update old config
            if (moduleConfiguration != null)
            {

                moduleConfiguration.CanView = item.CanView;
                moduleConfiguration.CanAdd = item.CanAdd;
                moduleConfiguration.CanEdit = item.CanEdit;
                moduleConfiguration.CanDelete = item.CanDelete;

                _context.RoleModuleConfiguration.Update(moduleConfiguration);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // add new configuration
                var roleModuleConfiguration = new Domain.RoleModuleConfiguration
                {
                    RoleID = role.Id,
                    ModuleID = item.ModuleId,
                    CanView = item.CanView,
                    CanAdd = item.CanAdd,
                    CanEdit = item.CanEdit,
                    CanDelete = item.CanDelete,
                    IsDeleted = false,
                };

                _context.RoleModuleConfiguration.Add(roleModuleConfiguration);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return new EditRoleResponse
        {
            RoleId = role.Id,
            RoleName = role.RoleName,
            Description = role.Description,
            IsDeleted = role.IsDeleted,
            ModuleConfiguration = request.Modules,
        };
    }
}
