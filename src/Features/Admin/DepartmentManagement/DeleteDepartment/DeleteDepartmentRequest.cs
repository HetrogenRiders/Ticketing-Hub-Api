using MediatR;

namespace TicketingHub.Api.Features.Admin.DepartmentManagement.DeleteDepartment
{
    public class DeleteDepartmentRequest : IRequest<DeleteDepartmentResponse>
    {
        public Guid DepartmentId { get; set; }
    }
}
