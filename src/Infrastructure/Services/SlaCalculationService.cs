using System;
using TicketingHub.Api.Common.Interfaces;

namespace TicketingHub.Api.Infrastructure.Services
{
    /// <summary>
    /// Simple SLA calculator.  Business rules (weekend skip, holidays) can be added here later.
    /// </summary>
    public class SlaCalculationService : ISlaCalculationService
    {
        public DateTime CalculateDueDate(DateTime createdAt, int responseHours, int resolutionHours)
        {
            // Simple default behaviour: dueDate = createdAt + resolutionHours (if > 0) else responseHours
            var total = resolutionHours > 0 ? resolutionHours : responseHours;
            return createdAt.AddHours(total);
        }

        public (DateTime? level1, DateTime? level2) CalculateEscalationTimes(DateTime createdAt, SLAInfo sla)
        {
            DateTime? l1 = sla.EscalationLevel1Hours > 0 ? createdAt.AddHours(sla.EscalationLevel1Hours) : (DateTime?)null;
            DateTime? l2 = sla.EscalationLevel2Hours > 0 ? createdAt.AddHours(sla.EscalationLevel2Hours) : (DateTime?)null;
            return (l1, l2);
        }
    }
}
