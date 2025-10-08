using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Authentication
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenValidator(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            RuleFor(v => v.AccessToken)
                .NotEmpty()
                .WithMessage("Access token is required.")
                .Must(BeAValidAccessToken)
                .WithMessage("Invalid access token.");

            RuleFor(v => v.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required.");
        }

        private bool BeAValidAccessToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            return principal != null; // Return true if valid
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero, // Remove any clock skew
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])) // Provide the symmetric key
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch (SecurityTokenException)
            {
                return null; // Return null if token is invalid
            }
            catch (ArgumentException)
            {
                return null; // Return null if token format is incorrect
            }
        }
    }
}
