using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.RoleManagement.GetRoleById
{
    public class GetRoleByIdValidator : AbstractValidator<GetRoleByIdRequest>
    {
        public GetRoleByIdValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage(localizer["Role ID is required."]);
        }
    }
}
