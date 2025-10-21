using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Infrastructure.Services;
using TicketingHub.Api.Resources; // if same namespace

namespace TicketingHub.Api.Infrastructure.Services
{
    public class SlaMonitoringService : ISlaMonitoringService
    {
        private readonly DBContext _context;
        private readonly ILogger<SlaMonitoringService> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly INotificationPublisher _notificationPublisher; // abstraction for sending notifications

        public SlaMonitoringService(DBContext context,
                                    ILogger<SlaMonitoringService> logger,
                                    IStringLocalizer<SharedResource> localizer,
                                    INotificationPublisher notificationPublisher)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
            _notificationPublisher = notificationPublisher;
        }

        public async Task MonitorSlaBreachesAsync()
        {
            var now = DateTime.UtcNow;

            // Find tickets with due date in the past and not yet escalated
            var breached = await _context.Tickets
                .Where(t => !t.IsDeleted && !t.IsEscalated && t.DueDate != null && t.DueDate < now)
                .ToListAsync();

            if (!breached.Any()) return;

            foreach (var ticket in breached)
            {
                try
                {
                    ticket.IsEscalated = true;
                    ticket.LastModified = DateTime.UtcNow;

                    // add ticket history
                    _context.TicketHistories.Add(new TicketHistory
                    {
                        Id = Guid.NewGuid(),
                        TicketId = ticket.Id,
                        ActionType = "SLA Breach",
                        OldValue = null,
                        NewValue = null,
                        ChangedById = Guid.Empty,
                        LastModified = DateTime.UtcNow
                    });

                    // publish notifications to assignee and subscribers (non-blocking)
                    var message = _localizer["Ticket SLA breached."];
                    await _notificationPublisher.PublishTicketSlaBreachAsync(ticket.Id, message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling SLA breach for ticket {TicketId}", ticket.Id);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
