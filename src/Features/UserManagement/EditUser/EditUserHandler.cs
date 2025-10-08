using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement;

public sealed class EditUserHandler(DBContext context) : IRequestHandler<EditUserRequest, EditUserResponse>
{
    private readonly DBContext _context = context;

    public async Task<EditUserResponse> Handle(EditUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.User.FindAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            throw new Exception("User not found!");
        }

        if (request.FirstName != null)
            user.FirstName = request.FirstName;
        if (request.LastName != null)
            user.LastName = request.LastName;
        user.EmailId = request.EmailId;
        if (request.Password != null)
            user.Password = CreatePasswordHash(request.Password);

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

        // add new user roles
        var newRoles = request.RoleId.Select(roleId => new UserRole
        {
            UserID = request.UserId,
            RoleID = roleId
        }).ToList();

        _context.UserRoles.AddRange(newRoles);
        await _context.SaveChangesAsync(cancellationToken);

        return new EditUserResponse
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailId = request.EmailId,
            RoleId = request.RoleId,
        };
    }

    private string CreatePasswordHash(string password)
    {
        // Hash the incoming password using SHA256
        using (var sha256 = SHA256.Create())
        {
            var hashedInputPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedInputPassword);
        }
    }
}
