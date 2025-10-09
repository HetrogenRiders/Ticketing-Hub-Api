
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingHub.Api.Features.Authentication.ExternalLogin
{
    public class ExternalLoginCommandRequest : IRequest<ExternalLoginResponse>
    {
        [Required]
        public string Provider { get; set; } = string.Empty; // e.g., "AzureAD", "Google"

        [Required]
        public string Subject { get; set; } = string.Empty;  // Provider user ID (unique)

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;    // User's verified email

        [MaxLength(150)]
        public string? FullName { get; set; }

        [Required]
        public string IdToken { get; set; } = string.Empty;  // Raw provider token
    }
}
