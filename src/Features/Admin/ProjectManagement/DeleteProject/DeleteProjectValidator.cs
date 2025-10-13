using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.DeleteProject
{
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectRequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteProjectValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage(localizer["Project ID is required."])
                .MustAsync(ProjectExists).WithMessage(localizer["Project not found."]);
        }

        private async Task<bool> ProjectExists(Guid projectId, CancellationToken token)
        {
            return await _context.Projects.AsNoTracking()
                .AnyAsync(p => p.Id == projectId, token);
        }
    }
}
