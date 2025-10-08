namespace TicketingHub.Api.Features.UserManagement;

public class EditUserResponse
{
    public Guid UserId { get; set; }
    public string EmailId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<Guid> RoleId { get; set; }
}