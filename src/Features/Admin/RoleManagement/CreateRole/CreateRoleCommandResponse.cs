namespace TicketingHub.Api.Features.Admin.RoleManagement.CreateRole
{
    public class CreateRoleCommandResponse
    {
        public Guid RoleId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
