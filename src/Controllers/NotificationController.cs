using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Ticketing.NotificationManagement.CreateNotification;
using TicketingHub.Api.Features.Ticketing.NotificationManagement.GetUserNotifications;
using TicketingHub.Api.Features.Ticketing.NotificationManagement.MarkAsRead;

namespace TicketingHub.Api.Controllers
{
    [Authorize]
    public class NotificationController : ApiControllerBase
    {
        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<GetUserNotificationsResponse>> GetUserNotifications(Guid userId)
        {
            return await Mediator.Send(new GetUserNotificationsRequest { UserId = userId });
        }

        [HttpPost("mark-as-read")]
        public async Task<ActionResult<MarkAsReadResponse>> MarkAsRead(MarkAsReadRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateNotificationResponse>> Create(CreateNotificationRequest request)
        {
            return await Mediator.Send(request);
        }
    }
}
