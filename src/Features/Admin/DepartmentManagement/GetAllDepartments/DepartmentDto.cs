namespace TicketingHub.Api.Features.Admin.DepartmentManagement.GetAllDepartments
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
