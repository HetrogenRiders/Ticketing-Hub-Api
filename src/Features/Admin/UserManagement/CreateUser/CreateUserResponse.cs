namespace TicketingHub.Api.Features.Admin.UserManagement.CreateUser
{
    public class CreateUserResponse
    {
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
