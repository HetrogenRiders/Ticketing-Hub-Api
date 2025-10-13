using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.DeleteStatus
{
    public class DeleteStatusHandler : IRequestHandler<DeleteStatusRequest, DeleteStatusResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteStatusHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<DeleteStatusResponse> Handle(DeleteStatusRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.Statuses.FirstAsync(p => p.Id == request.StatusId, cancellationToken);

            _context.Statuses.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteStatusResponse { Message = _localizer["Status deleted successfully."] };
        }
    }
}
