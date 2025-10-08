namespace TicketingHub.Api.Features.Authentication;

public class AuthenticateUserResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? Expiration { get; set; }
    public Guid? Role { get; set; }
}

public class UserRoleClaims
{
    public string Role { get; set; }
    public string Module { get; set; }
    public Permission Permission { get; set; }
}

public class Permission
{
    public bool? CanView { get; set; }
    public bool? CanAdd { get; set; }
    public bool? CanEdit { get; set; }
    public bool? CanDelete { get; set; }
}