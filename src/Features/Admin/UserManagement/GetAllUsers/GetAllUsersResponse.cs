namespace TicketingHub.Api.Features.Admin.UserManagement.GetAllUsers
{
   

    public class GetAllUsersResponse
    {
        public List<UserDto> Users { get; set; } = new();
    }
}
