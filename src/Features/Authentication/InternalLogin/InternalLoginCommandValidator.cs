using FluentValidation;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Authentication.InternalLogin
{
    public class InternalLoginCommandValidator : AbstractValidator<InternalLoginRequest>
    {
        public InternalLoginCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer["EmailRequired"])
                .EmailAddress().WithMessage(localizer["ValidEmailRequired"]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localizer["PasswordRequired"])
                .MinimumLength(6).WithMessage(localizer["PasswordTooShort"]);
        }
    }
}
