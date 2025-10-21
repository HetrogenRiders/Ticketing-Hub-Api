using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.MarkAsRead
{
    public class MarkAsReadHandler : IRequestHandler<MarkAsReadRequest, MarkAsReadResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public MarkAsReadHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<MarkAsReadResponse> Handle(MarkAsReadRequest request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.NotificationId, cancellationToken);

            if (notification == null)
                throw new KeyNotFoundException(_localizer["Notification not found."]);

            notification.IsRead = true;
            await _context.SaveChangesAsync(cancellationToken);

            return new MarkAsReadResponse { Success = true };
        }
    }
}
