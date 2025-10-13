using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.RoleManagement.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateRoleCommandHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId && !r.IsDeleted, cancellationToken);

            if (role == null)
                throw new KeyNotFoundException(_localizer["Role not found."]);

            bool duplicate = await _context.Roles
                .AnyAsync(r => r.RoleName.ToLower() == request.RoleName.ToLower() && r.Id != request.RoleId && !r.IsDeleted, cancellationToken);

            if (duplicate)
                throw new InvalidOperationException(_localizer["Another role with this name already exists."]);

            role.RoleName = request.RoleName.Trim();
            role.Description = request.Description;
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateRoleCommandResponse
            {
                Message = _localizer["Role updated successfully."]
            };
        }
    }
}
