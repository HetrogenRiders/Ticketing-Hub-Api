using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.RoleManagement.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateRoleCommandHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var exists = await _context.Roles
                .AnyAsync(r => r.RoleName.ToLower() == request.RoleName.ToLower() && !r.IsDeleted, cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["Role name already exists."]);

            var role = new Role
            {
                Id = Guid.NewGuid(),
                RoleName = request.RoleName.Trim(),
                Description = request.Description,
                IsDeleted = false
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateRoleCommandResponse
            {
                RoleId = role.Id,
                Message = _localizer["Role created successfully."]
            };
        }
    }
}
