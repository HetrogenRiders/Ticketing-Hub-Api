
using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Authentication.ExternalLogin
{
    public class ExternalLoginValidator : AbstractValidator<ExternalLoginCommandRequest>
    {
        public ExternalLoginValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Provider)
                .NotEmpty().WithMessage(localizer["ProviderRequired"]);

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage(localizer["SubjectRequired"]);

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress()
                .WithMessage(localizer["ValidEmailRequired"]);

            RuleFor(x => x.IdToken)
                .NotEmpty().WithMessage(localizer["IdTokenRequired"]);
        }
    }
}
