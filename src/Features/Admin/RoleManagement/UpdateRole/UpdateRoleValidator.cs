using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.RoleManagement.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommandRequest>
    {
        public UpdateRoleValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(localizer["Role ID is required."]);

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(localizer["Role name is required."])
                .MaximumLength(100).WithMessage(localizer["Role name cannot exceed 100 characters."]);
        }
    }
}
