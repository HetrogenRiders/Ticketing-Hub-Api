using System.Threading.Tasks;

namespace TicketingHub.Api.Common.Interfaces
{
    public interface ISlaMonitoringService
    {
        /// <summary>
        /// Scans for SLA breaches and processes escalations.
        /// </summary>
        Task MonitorSlaBreachesAsync();
    }
}
