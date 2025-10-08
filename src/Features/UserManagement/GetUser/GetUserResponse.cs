namespace TicketingHub.Api.Features.UserManagement
{
    public class GetUserResponse
    {
        public UserResponse User { get; set; }
    }

    public class UserResponse
    {
        public Guid UserID { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Roles> Roles { get; set; }
    }
}