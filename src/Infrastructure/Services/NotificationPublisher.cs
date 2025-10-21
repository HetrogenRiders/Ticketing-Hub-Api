using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Features.Ticketing.NotificationManagement;

namespace TicketingHub.Api.Infrastructure.Services
{
    public class NotificationPublisher : INotificationPublisher
    {
        private readonly IHubContext<NotificationHub> _hub;
        private readonly IEmailService _emailService; // if you want emails too
        private readonly ILogger<NotificationPublisher> _logger;

        public NotificationPublisher(IHubContext<NotificationHub> hub, IEmailService emailService, ILogger<NotificationPublisher> logger)
        {
            _hub = hub;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task PublishTicketSlaBreachAsync(Guid ticketId, string message)
        {
            try
            {
                // Real-time: broadcast to groups or ticket-specific users
                await _hub.Clients.Group($"ticket-{ticketId}").SendAsync("TicketSlaBreach", new { TicketId = ticketId, Message = message });
                // Optionally send email to relevant recipients (resolve emails list outside)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing SLA breach notification for {TicketId}", ticketId);
            }
        }

        public async Task PublishTicketUpdatedAsync(Guid ticketId, string message)
        {
            await _hub.Clients.Group($"ticket-{ticketId}").SendAsync("TicketUpdated", new { TicketId = ticketId, Message = message });
        }
    }
}
