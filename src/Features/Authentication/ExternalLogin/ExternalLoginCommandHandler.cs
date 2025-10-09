
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Common.Interfaces;

namespace TicketingHub.Api.Features.Authentication.ExternalLogin
{
    public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommandRequest, ExternalLoginResponse>
    {
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtService;
        private readonly ILogger<ExternalLoginCommandHandler> _logger;

        public ExternalLoginCommandHandler(DBContext context, IJwtTokenService jwtService, ILogger<ExternalLoginCommandHandler> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<ExternalLoginResponse> Handle(ExternalLoginCommandRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
                throw new Exception("Email not returned from external provider.");

            _logger.LogInformation("External SSO login attempt: Provider={Provider}, Email={Email}", request.Provider, request.Email);

            // Find user by email
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            bool isNewUser = false;

            // Register new user if not exists
            if (user == null)
            {
                var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Employee", cancellationToken)
                    ?? throw new Exception("Default Employee role not found.");

                user = new User
                {
                    Email = request.Email,
                    FullName = request.FullName ?? request.Email,
                    EmployeeCode = $"SSO-{Guid.NewGuid().ToString()[..8]}",
                    RoleId = defaultRole.Id,
                    IsActive = true,
                    Created = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                isNewUser = true;
            }
            else
            {
                // Update user info if necessary
                if (!string.IsNullOrEmpty(request.FullName) && user.FullName != request.FullName)
                {
                    user.FullName = request.FullName!;
                    user.LastModified = DateTime.UtcNow;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            // Generate JWT
            var role = user.Role?.RoleName ?? "Employee";
            var token = _jwtService.GenerateToken(user.Id, user.Email, role);

            return new ExternalLoginResponse
            {
                Token = token,
                Email = user.Email,
                Role = role,
                IsNewUser = isNewUser
            };
        }
    }
}
