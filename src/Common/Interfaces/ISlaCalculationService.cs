using System;

namespace TicketingHub.Api.Common.Interfaces
{
    public interface ISlaCalculationService
    {
        /// <summary>
        /// Calculates due date given createdAt and SLA numeric values.
        /// </summary>
        DateTime CalculateDueDate(DateTime createdAt, int responseHours, int resolutionHours);

        /// <summary>
        /// Optional helper to compute escalation timestamps for an SLA.
        /// Returns tuple: (level1Time, level2Time)
        /// </summary>
        (DateTime? level1, DateTime? level2) CalculateEscalationTimes(DateTime createdAt, SLAInfo sla);
    }

    public record SLAInfo(int ResponseHours, int ResolutionHours, int EscalationLevel1Hours, int EscalationLevel2Hours);
}
