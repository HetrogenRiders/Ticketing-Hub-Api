using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Infrastructure.Services;

namespace TicketingHub.Api.Features.Authentication.InternalLogin
{
    public class InternalLoginCommandHandler : IRequestHandler<InternalLoginRequest, InternalLoginResponse>
    {
        private readonly DBContext _db;
        private readonly IJwtTokenService _jwt;
        private readonly IConfiguration _config;

        public InternalLoginCommandHandler(DBContext db, IJwtTokenService jwt, IConfiguration config)
        {
            _db = db;
            _jwt = jwt;
            _config = config;
        }

        public async Task<InternalLoginResponse> Handle(InternalLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive && !u.IsDeleted, cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            if (!VerifyPasswordHash(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var roleName = user.Role?.RoleName ?? "User";

            var permissions = await _db.RolePermissions
                .Where(rp => rp.RoleId == user.RoleId)
                .Include(rp => rp.PermissionType)
                .Select(rp => rp.PermissionType!.PermissionName)
                .ToListAsync(cancellationToken);

            var token = _jwt.GenerateToken(user.Id, user.Email, roleName, permissions);

            return new InternalLoginResponse
            {
                Token = token,
                FullName = user.FullName,
                RoleName = roleName,
                Permissions = permissions
            };
        }

        private bool VerifyPasswordHash(string plain, string? storedHash)
        {
            if (string.IsNullOrEmpty(storedHash)) return false;
            using var sha = SHA256.Create();
            var hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(plain)));
            return hash == storedHash;
        }
    }
}
