namespace TicketingHub.Api.Features.UserManagement;

public class CreateUserResponse
{
    public Guid UserId { get; set; }
    public string EmailId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<Guid> RoleId { get; set; }
}
