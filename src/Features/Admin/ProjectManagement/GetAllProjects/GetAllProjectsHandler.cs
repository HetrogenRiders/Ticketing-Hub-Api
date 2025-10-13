using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.GetAllProjects
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsRequest, GetAllProjectsResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetAllProjectsHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetAllProjectsResponse> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .Include(p => p.Department)
                .Include(p => p.Manager)
                .AsNoTracking()
                .OrderBy(p => p.ProjectName)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    DepartmentName = p.Department != null ? p.Department.DepartmentName : string.Empty,
                    ManagerName = p.Manager != null ? p.Manager.FullName : string.Empty,
                    IsActive = p.IsActive
                })
                .ToListAsync(cancellationToken);

            return new GetAllProjectsResponse { Projects = projects };
        }
    }
}
