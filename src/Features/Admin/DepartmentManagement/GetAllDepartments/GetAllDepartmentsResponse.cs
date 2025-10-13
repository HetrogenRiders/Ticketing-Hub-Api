using System.Collections.Generic;

namespace TicketingHub.Api.Features.Admin.DepartmentManagement.GetAllDepartments
{   

    public class GetAllDepartmentsResponse
    {
        public List<DepartmentDto> Departments { get; set; } = new();
    }
}
