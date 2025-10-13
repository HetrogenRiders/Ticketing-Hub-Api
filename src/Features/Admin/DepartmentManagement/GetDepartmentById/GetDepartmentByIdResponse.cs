namespace TicketingHub.Api.Features.Admin.DepartmentManagement.GetDepartmentById
{
    public class GetDepartmentByIdResponse
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public Guid? ParentDepartmentId { get; set; }
        public Guid? HeadUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
