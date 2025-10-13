using MediatR;

namespace TicketingHub.Api.Features.Admin.DepartmentManagement.UpdateDepartment
{
    public class UpdateDepartmentRequest : IRequest<UpdateDepartmentResponse>
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public Guid? ParentDepartmentId { get; set; }
        public Guid? HeadUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
