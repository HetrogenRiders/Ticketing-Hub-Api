namespace TicketingHub.Api.Features.UserManagement
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public Guid UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<Roles> Roles { get; set; }
    }

    public class Roles
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
