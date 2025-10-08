using MediatR;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.UserManagement;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly DBContext _context;

    public CreateUserHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserID = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailId = request.EmailId,
            Password = CreatePasswordHash(request.Password),
            IsDeleted = false
        };

        _context.User.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);

        var userRoles = request.RoleId.Select(roleId => new UserRole
        {
            UserID = newUser.UserID,
            RoleID = roleId
        }).ToList();

        _context.UserRoles.AddRange(userRoles);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse
        {
            UserId = newUser.UserID,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailId = request.EmailId,
            IsDeleted = newUser.IsDeleted,
            RoleId = request.RoleId
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
