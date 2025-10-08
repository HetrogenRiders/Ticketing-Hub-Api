
namespace TicketingHub.Api.Domain;

public class RefreshToken
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}

