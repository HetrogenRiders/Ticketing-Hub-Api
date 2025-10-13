using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.RoleManagement.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommandRequest>
    {
        public CreateRoleValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(localizer["Role name is required."])
                .MaximumLength(100).WithMessage(localizer["Role name cannot exceed 100 characters."]);
        }
    }
}
