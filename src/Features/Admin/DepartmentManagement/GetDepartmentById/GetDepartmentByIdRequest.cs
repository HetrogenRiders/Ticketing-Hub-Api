using MediatR;

namespace TicketingHub.Api.Features.Admin.DepartmentManagement.GetDepartmentById
{
    public class GetDepartmentByIdRequest : IRequest<GetDepartmentByIdResponse>
    {
        public Guid DepartmentId { get; set; }
    }
}
