using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.RoleManagement.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteRoleCommandHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId && !r.IsDeleted, cancellationToken);

            if (role == null)
                throw new KeyNotFoundException(_localizer["Role not found."]);

            role.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteRoleCommandResponse
            {
                Message = _localizer["Role deleted successfully."]
            };
        }
    }
}
