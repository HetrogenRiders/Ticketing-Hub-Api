using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetAllPriority
{
    public class GetAllPriorityHandler : IRequestHandler<GetAllPriorityRequest, GetAllPriorityResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetAllPriorityHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetAllPriorityResponse> Handle(GetAllPriorityRequest request, CancellationToken cancellationToken)
        {
            var items = await _context.Priorities
                .AsNoTracking()
                .OrderBy(x => x.PriorityName)
                .Select(x => new PriorityDto { Id = x.Id, PriorityName = x.PriorityName, IsActive = x.IsActive })
                .ToListAsync(cancellationToken);

            return new GetAllPriorityResponse { PriorityDtos = items };
        }
    }
}
