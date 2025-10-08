namespace TicketingHub.Api.Features.Authentication;

public class RefreshTokenResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? Expiration { get; set; }
    public Guid? Role { get; set; }
}
