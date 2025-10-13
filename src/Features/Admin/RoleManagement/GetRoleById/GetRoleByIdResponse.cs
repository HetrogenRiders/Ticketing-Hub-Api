namespace TicketingHub.Api.Features.Admin.RoleManagement.GetRoleById
{
    public class GetRoleByIdResponse
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
