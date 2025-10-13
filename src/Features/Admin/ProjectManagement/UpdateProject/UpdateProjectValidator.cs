using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateProjectValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage(localizer["Project ID is required."])
                .MustAsync(ProjectExists).WithMessage(localizer["Project not found."]);

            RuleFor(x => x.ProjectName)
                .NotEmpty().WithMessage(localizer["Project name is required."])
                .MaximumLength(150).WithMessage(localizer["Project name cannot exceed 150 characters."]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department is required."]);

            RuleFor(x => x.ManagerId)
                .NotEmpty().WithMessage(localizer["Manager is required."]);
        }

        private async Task<bool> ProjectExists(Guid projectId, CancellationToken token)
        {
            return await _context.Projects.AsNoTracking()
                .AnyAsync(p => p.Id == projectId, token);
        }
    }
}
