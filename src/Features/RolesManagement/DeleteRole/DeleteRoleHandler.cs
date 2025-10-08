using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.RolesManagement
{

    public sealed class DeleteRoleHandler : IRequestHandler<DeleteRoleRequest, DeleteRoleResponse>
    {
        private readonly DBContext _context;

        public DeleteRoleHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<DeleteRoleResponse> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { request.RoleId }, cancellationToken);

            if (role == null)
            {
                return new DeleteRoleResponse
                {
                    Success = false,
                    Message = "Role not found"
                };
            }

            role.IsDeleted = true;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(cancellationToken);

            // delete configuration for the role
            var moduleConfiguration = await _context.RoleModuleConfiguration.Where(x => x.RoleID == request.RoleId)
                .ToListAsync(cancellationToken);

            if (moduleConfiguration != null)
            {
                moduleConfiguration.ForEach(x => x.IsDeleted = true);
                _context.RoleModuleConfiguration.UpdateRange(moduleConfiguration);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new DeleteRoleResponse
            {
                Success = true,
                Message = "Role deleted successfully"
            };
        }

    }
}
