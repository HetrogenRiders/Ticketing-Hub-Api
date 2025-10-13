using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;


namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.UpdatePriority
{
    public class UpdatePriorityHandler : IRequestHandler<UpdatePriorityRequest, UpdatePriorityResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdatePriorityHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdatePriorityResponse> Handle(UpdatePriorityRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.Priorities.FirstAsync(p => p.Id == request.PriorityId, cancellationToken);

            // update fields
            item.PriorityName = request.PriorityName?.Trim();
            item.SLAHours = request.SLAHours;
            item.ColorCode = request.ColorCode?.Trim();
            item.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdatePriorityResponse
            {
                Message = _localizer["Priority updated successfully."]
            };
        }
    }
}
