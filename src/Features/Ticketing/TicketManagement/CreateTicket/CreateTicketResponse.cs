using System;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.CreateTicket
{
    public class CreateTicketResponse
    {
        public Guid TicketId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
