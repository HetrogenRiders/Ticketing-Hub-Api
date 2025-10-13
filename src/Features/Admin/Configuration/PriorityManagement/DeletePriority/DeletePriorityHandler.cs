using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;


namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.DeletePriority
{
    public class DeletePriorityHandler : IRequestHandler<DeletePriorityRequest, DeletePriorityResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeletePriorityHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<DeletePriorityResponse> Handle(DeletePriorityRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.Priorities.FirstAsync(p => p.Id == request.PriorityId, cancellationToken);

            _context.Priorities.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeletePriorityResponse { Message = _localizer["Priority deleted successfully."] };
        }
    }
}
