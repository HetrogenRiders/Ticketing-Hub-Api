using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.RolesManagement;

public sealed class CreateRoleHandler : IRequestHandler<CreateRoleRequest, CreateRoleResponse>
{
    private readonly DBContext _context;

    public CreateRoleHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var newRole = new Role
        {
            Id = Guid.NewGuid(),
            RoleName = request.RoleName,
            Description = request.Description,
            IsDeleted = false
        };

        _context.Roles.Add(newRole);
        await _context.SaveChangesAsync(cancellationToken);

        var modules = await _context.Modules.ToListAsync(cancellationToken);


        foreach (var item in request.Modules)
        {
            var roleModuleConfiguration = new Domain.RoleModuleConfiguration
            {
                RoleID = newRole.Id,
                ModuleID = item.ModuleId,
                CanView = item.CanView,
                CanAdd = item.CanAdd,
                CanEdit = item.CanEdit,
                CanDelete = item.CanDelete,
                IsDeleted = false,
            };

            _context.RoleModuleConfiguration.Add(roleModuleConfiguration);
            await _context.SaveChangesAsync(cancellationToken);

            item.ModuleName = modules.FirstOrDefault(x => x.Id == item.ModuleId).ModuleName;
        }


        return new CreateRoleResponse
        {
            RoleId = newRole.Id,
            RoleName = newRole.RoleName,
            Description = newRole.Description,
            IsDeleted = newRole.IsDeleted,
            Modules = request.Modules
        };
    }
}
