using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.GetUserNotifications
{
    public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsRequest, GetUserNotificationsResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetUserNotificationsHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetUserNotificationsResponse> Handle(GetUserNotificationsRequest request, CancellationToken cancellationToken)
        {
            var items = await _context.Notifications
                .Where(n => n.RecipientId == request.UserId)
                .OrderByDescending(n => n.Created)
                .Select(n => new UserNotificationDto
                {
                    Id = n.Id,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.Created
                })
                .ToListAsync(cancellationToken);

            return new GetUserNotificationsResponse { Notifications = items };
        }
    }
}
