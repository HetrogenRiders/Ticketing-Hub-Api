
namespace TicketingHub.Api.Common.Interfaces;
public interface ICurrentUserService
{
    string? UserId { get; }
    public List<string> Roles { get; }
}