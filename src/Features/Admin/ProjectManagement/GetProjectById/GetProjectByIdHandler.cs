using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdRequest, GetProjectByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetProjectByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetProjectByIdResponse> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

            return new GetProjectByIdResponse
            {
                ProjectId = project.Id,
                ProjectName = project.ProjectName,
                DepartmentId = project.DepartmentId,
                ManagerId = project.ManagerId ?? Guid.Empty,
                IsActive = project.IsActive
            };
        }
    }
}
