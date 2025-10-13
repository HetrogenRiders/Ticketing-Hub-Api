using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Features.Admin.DepartmentManagement.UpdateDepartment;


namespace TicketingHub.Api.Features.Admin.UserManagement.GetUserById
{

    public class GetUserByIdValidator : AbstractValidator<UpdateDepartmentRequest>
    {
        public GetUserByIdValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department ID is required."]);
        }
    }
}
