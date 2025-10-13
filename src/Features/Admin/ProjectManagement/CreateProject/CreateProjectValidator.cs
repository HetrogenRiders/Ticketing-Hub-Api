using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.ProjectManagement.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.ProjectName)
                .NotEmpty().WithMessage(localizer["Project name is required."])
                .MaximumLength(150).WithMessage(localizer["Project name cannot exceed 150 characters."]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department is required."]);

            RuleFor(x => x.ManagerId)
                .NotEmpty().WithMessage(localizer["Manager is required."]);
        }
    }
}
