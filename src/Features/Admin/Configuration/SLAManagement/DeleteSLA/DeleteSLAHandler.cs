using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.DeleteSLA
{
    public class DeleteSLAHandler : IRequestHandler<DeleteSLARequest, DeleteSLAResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteSLAHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<DeleteSLAResponse> Handle(DeleteSLARequest request, CancellationToken cancellationToken)
        {
            var item = await _context.SLAs.FirstAsync(p => p.Id == request.SLAId, cancellationToken);

            _context.SLAs.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteSLAResponse { Message = _localizer["SLA deleted successfully."] };
        }
    }
}
