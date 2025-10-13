using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.UserManagement.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateUserHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException(_localizer["User not found."]);

            user.FullName = request.FullName.Trim();
            user.DepartmentId = request.DepartmentId;
            user.RoleId = request.RoleId;
            user.ManagerId = request.ManagerId;
            user.IsActive = request.IsActive;
            user.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateUserResponse
            {
                Message = _localizer["User updated successfully."]
            };
        }
    }
}
