using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetAllStatus
{
    public class GetAllStatusHandler : IRequestHandler<GetAllStatusRequest, GetAllStatusResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetAllStatusHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetAllStatusResponse> Handle(GetAllStatusRequest request, CancellationToken cancellationToken)
        {
            var items = await _context.Statuses
                .AsNoTracking()
                .OrderBy(x => x.StatusName)
                .Select(x => new StatusDto { Id = x.Id, StatusName = x.StatusName, IsActive = x.IsActive })
                .ToListAsync(cancellationToken);

            return new GetAllStatusResponse { StatusDtos = items };
        }
    }
}
