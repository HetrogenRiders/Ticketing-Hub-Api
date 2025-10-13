using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, CreateProjectResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateProjectHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateProjectResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            bool exists = await _context.Projects
                .AnyAsync(p => p.ProjectName.ToLower() == request.ProjectName.ToLower(), cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["Project name already exists."]);

            var project = new Project
            {
                Id = Guid.NewGuid(),
                ProjectName = request.ProjectName.Trim(),
                DepartmentId = request.DepartmentId,
                ManagerId = request.ManagerId,
                IsActive = request.IsActive,
                Created = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateProjectResponse
            {
                ProjectId = project.Id,
                Message = _localizer["Project created successfully."]
            };
        }
    }
}
