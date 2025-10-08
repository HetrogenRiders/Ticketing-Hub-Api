using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Infrastructure;
using Newtonsoft.Json;

namespace TicketingHub.Api.Features.Authentication
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenHandler(DBContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var username = principal.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _context.User.SingleOrDefaultAsync(u => u.EmailId == username, cancellationToken);
            var userRoles = await GetUserRoleAsync(user.UserID, cancellationToken);
            var claims = CreateClaims(user.UserID, username, userRoles, $"{user.FirstName} {user.LastName}");
            var newTokenString = GenerateJwtToken(claims);
            var newRefreshToken = GenerateRefreshToken();
            var roles = await GetRoles(user.UserID, cancellationToken);

            return new RefreshTokenResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = newTokenString,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpireDays"])),
                Role = roles.FirstOrDefault()
            };
        }

        private async Task<List<UserRoleClaims>> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken)
        {
            var roles = await _context.UserRoles
                    .Where(ur => ur.UserID == userId)
                    .Join(_context.Roles,
                          ur => ur.RoleID,
                          r => r.Id,
                          (ur, r) => r.RoleName)
                    .ToListAsync(cancellationToken);

            var roleModules = await _context.RoleModuleConfiguration
                    .Join(_context.Roles,
                          userRole => userRole.RoleID,
                          role => role.Id,
                          (userRole, role) => new { userRole, role })
                    .Join(_context.Modules,
                          userRole => userRole.userRole.ModuleID,
                          module => module.Id,
                          (userRole, module) => new { userRole, module, userRole.role })
                    .Select(x => new UserRoleClaims
                    {
                        Role = x.userRole.role.RoleName,
                        Module = x.module.ModuleName,
                        Permission = new Permission
                        {
                            CanView = x.userRole.userRole.CanView,
                            CanAdd = x.userRole.userRole.CanAdd,
                            CanEdit = x.userRole.userRole.CanEdit,
                            CanDelete = x.userRole.userRole.CanDelete,
                        }
                    })
                    .ToListAsync(cancellationToken);

            return roleModules
                  .Where(rm => roles.Contains(rm.Role))
                  .ToList();
        }

        private IEnumerable<Claim> CreateClaims(Guid userId, string username, List<UserRoleClaims> userRoles, string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.GivenName, name),
            };

            // Add roles as claims
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.Role)));

            // Add permission claims (canView, canAdd, canEdit, canDelete)
            var permissions = JsonConvert.SerializeObject(userRoles);
            claims.Add(new Claim("ModulePermission", permissions));

            return claims;
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false,
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }

        private async Task<List<Guid>> GetRoles(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                   .Where(ur => ur.UserID == userId)
                   .Select(x => x.RoleID)
                   .ToListAsync(cancellationToken);
        }
    }
}
