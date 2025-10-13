using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, UpdateProjectResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateProjectHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateProjectResponse> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

            project.ProjectName = request.ProjectName.Trim();
            project.DepartmentId = request.DepartmentId;
            project.ManagerId = request.ManagerId;
            project.IsActive = request.IsActive;
            project.Created = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateProjectResponse
            {
                Message = _localizer["Project updated successfully."]
            };
        }
    }
}
