using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Authentication;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserRequest, AuthenticateUserResponse>
{
    private readonly DBContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticateUserHandler(DBContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticateUserResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Username, cancellationToken);

        var userRoles = await GetUserRoleAsync(user.Id, cancellationToken);

        var claims = CreateClaims(user.Id, request.Username, userRoles, user.FullName);
        var tokenString = GenerateJwtToken(claims);
        var refreshToken = GenerateRefreshToken();
        var roles = await GetRoles(user.Id, cancellationToken);

        return new AuthenticateUserResponse
        {
            FullName = user.FullName,
            Token = tokenString,
            Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["jwt:ExpirationMinutes"])),
            RefreshToken = refreshToken,
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

    private IEnumerable<Claim> CreateClaims(Guid userId, string username, List<UserRoleClaims> userRole, string name)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.GivenName, name),
        };

        // Add roles as claims
        claims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role.Role)));

        // Add permission claims (canView, canAdd, canEdit, canDelete)
        var permissions = JsonConvert.SerializeObject(userRole);
        claims.Add(new Claim("ModulePermission", permissions));

        return claims;
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var expirationMenutes = Convert.ToInt32(_configuration["Jwt:ExpirationMinutes"]);
        var expirationTime = DateTime.UtcNow.AddMinutes(expirationMenutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _configuration["Jwt:Issuer"],
            Expires = expirationTime,
            Audience = _configuration["Jwt:Audience"],
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

    private async Task<List<Guid>> GetRoles(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.UserRoles
               .Where(ur => ur.UserID == userId)
               .Select(x => x.RoleID)
               .ToListAsync(cancellationToken);
    }
}
