using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.ProjectManagement.CreateProject;
using TicketingHub.Api.Features.Admin.ProjectManagement.UpdateProject;
using TicketingHub.Api.Features.Admin.ProjectManagement.DeleteProject;
using TicketingHub.Api.Features.Admin.ProjectManagement.GetAllProjects;

namespace TicketingHub.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/projects")]
    public class ProjectAdminController : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<CreateProjectResponse>> Create([FromBody] CreateProjectRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<ActionResult<UpdateProjectResponse>> Update(Guid id, [FromBody] UpdateProjectRequest request)
        {
            request.ProjectId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<DeleteProjectResponse>> Delete(Guid id)
        {
            var request = new DeleteProjectRequest { ProjectId = id };
            return await Mediator.Send(request);
        }

        [HttpGet("list")]
        public async Task<ActionResult<GetAllProjectsResponse>> GetAll()
        {
            var request = new GetAllProjectsRequest();
            return await Mediator.Send(request);
        }
    }
}
