using MediatR;

namespace TicketingHub.Api.Features.UserManagement;

public class EditUserRequest : IRequest<EditUserResponse>
{
    public Guid UserId { get; set; }
    public string EmailId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public IEnumerable<Guid> RoleId { get; set; }
}