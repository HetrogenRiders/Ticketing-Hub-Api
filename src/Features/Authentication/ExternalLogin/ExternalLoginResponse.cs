
namespace TicketingHub.Api.Features.Authentication.ExternalLogin
{
    public class ExternalLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsNewUser { get; set; }
    }
}
