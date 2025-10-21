using FluentValidation;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.CreateNotification
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationRequest>
    {
        public CreateNotificationValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.RecipientId)
                .NotEmpty().WithMessage(localizer["Recipient ID is required."]);

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage(localizer["Message cannot be empty."])
                .MaximumLength(500).WithMessage(localizer["Message cannot exceed 500 characters."]);

            RuleFor(x => x.NotificationType)
                .NotEmpty().WithMessage(localizer["Notification type is required."])
                .MaximumLength(50);
        }
    }
}
