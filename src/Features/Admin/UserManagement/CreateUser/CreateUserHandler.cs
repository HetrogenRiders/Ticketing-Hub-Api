using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.UserManagement.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateUserHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email.ToLower() == request.Email.ToLower() || u.EmployeeCode == request.EmployeeCode, cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["User with this email or employee code already exists."]);

            // Hash password
            using var sha = SHA256.Create();
            var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            var passwordHash = Convert.ToBase64String(hashBytes);

            var user = new User
            {
                Id = Guid.NewGuid(),
                EmployeeCode = request.EmployeeCode,
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                DepartmentId = request.DepartmentId,
                RoleId = request.RoleId,
                ManagerId = request.ManagerId,
                IsActive = request.IsActive,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserResponse
            {
                UserId = user.Id,
                Message = _localizer["User created successfully."]
            };
        }
    }
}
