using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.DepartmentManagement.CreateDepartment;
using TicketingHub.Api.Features.Admin.DepartmentManagement.UpdateDepartment;
using TicketingHub.Api.Features.Admin.DepartmentManagement.DeleteDepartment;
using TicketingHub.Api.Features.Admin.DepartmentManagement.GetAllDepartments;
using TicketingHub.Api.Features.Admin.DepartmentManagement.GetDepartmentById;

namespace TicketingHub.Api.Controllers.Admin
{
    /// <summary>
    /// Handles administrative operations for Department Management.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/admin/departments")]
    public class DepartmentAdminController : ApiControllerBase
    {
        /// <summary>
        /// Returns all departments.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllDepartmentsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAllDepartmentsResponse>> GetAll()
        {
            return await Mediator.Send(new GetAllDepartmentsRequest());
        }

        /// <summary>
        /// Returns details of a specific department by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetDepartmentByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetDepartmentByIdResponse>> GetById(Guid id)
        {
            var request = new GetDepartmentByIdRequest { DepartmentId = id };
            return await Mediator.Send(request);
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateDepartmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateDepartmentResponse>> Create([FromBody] CreateDepartmentRequest request)
        {
            var result = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = result.DepartmentId }, result);
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UpdateDepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateDepartmentResponse>> Update(Guid id, [FromBody] UpdateDepartmentRequest request)
        {
            request.DepartmentId = id;
            return await Mediator.Send(request);
        }

        /// <summary>
        /// Deletes a department by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(DeleteDepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeleteDepartmentResponse>> Delete(Guid id)
        {
            var request = new DeleteDepartmentRequest { DepartmentId = id };
            return await Mediator.Send(request);
        }
    }
}
