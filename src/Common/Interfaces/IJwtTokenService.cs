namespace TicketingHub.Api.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userId, string email, string role, IEnumerable<string>? permissions = null);
    }
}
