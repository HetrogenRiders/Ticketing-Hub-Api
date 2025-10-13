using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.RoleManagement.GetRoleById
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetRoleByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetRoleByIdResponse> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == request.RoleId && !r.IsDeleted, cancellationToken);

            if (role == null)
                throw new KeyNotFoundException(_localizer["Role not found."]);

            return new GetRoleByIdResponse
            {
                RoleId = role.Id,
                RoleName = role.RoleName,
                Description = role.Description
            };
        }
    }
}
