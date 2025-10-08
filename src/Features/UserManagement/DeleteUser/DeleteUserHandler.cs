using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement
{

    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly DBContext _context;

        public DeleteUserHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FindAsync(new object[] { request.UserId }, cancellationToken);

            if (user == null)
            {
                return new DeleteUserResponse
                {
                    Success = false,
                    Message = "User not found!"
                };
            }

            user.IsDeleted = true;
            _context.User.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            // delete existing user roles
            var userRoles = await _context.UserRoles
            .Where(ur => ur.UserID == request.UserId)  // Filter UserRoles by UserID
            .ToListAsync(cancellationToken);   // Get the list of roles for the user

            if (userRoles.Any())
            {
                _context.UserRoles.RemoveRange(userRoles);  // Remove all roles for the user
                await _context.SaveChangesAsync(cancellationToken);  // Save changes to the database
            }

            return new DeleteUserResponse
            {
                Success = true,
                Message = "User deleted successfully."
            };
        }

    }
}
