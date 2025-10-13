namespace TicketingHub.Api.Features.Admin.UserManagement.GetUserById
{
    public class GetUserByIdResponse
    {
        public Guid UserId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Guid RoleId { get; set; }
        public Guid? ManagerId { get; set; }
        public bool IsActive { get; set; }
    }
}
