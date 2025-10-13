using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, DeleteProjectResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteProjectHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<DeleteProjectResponse> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);


            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteProjectResponse
            {
                Message = _localizer["Project deleted successfully."]
            };
        }
    }
}
