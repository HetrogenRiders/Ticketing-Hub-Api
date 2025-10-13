using MediatR;

namespace TicketingHub.Api.Features.Admin.DepartmentManagement.CreateDepartment
{
    public class CreateDepartmentRequest : IRequest<CreateDepartmentResponse>
    {
        public string DepartmentName { get; set; } = string.Empty;
        public Guid? ParentDepartmentId { get; set; }
        public Guid? HeadUserId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
