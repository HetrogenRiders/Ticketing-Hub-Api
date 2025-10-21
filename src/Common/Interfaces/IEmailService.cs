using System.Threading.Tasks;
using System.Collections.Generic;

namespace TicketingHub.Api.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(IEnumerable<string> recipients, string subject, string body);
    }
}
