using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.DeleteDepartment
{   

    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentRequest>
    {
        public DeleteDepartmentValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department ID is required."]);
        }
    }
}
