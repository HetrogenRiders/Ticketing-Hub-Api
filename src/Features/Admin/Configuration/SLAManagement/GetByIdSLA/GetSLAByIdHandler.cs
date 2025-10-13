using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetByIdSLA
{
    public class GetSLAByIdHandler : IRequestHandler<GetSLAByIdRequest, GetSLAByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetSLAByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetSLAByIdResponse> Handle(GetSLAByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.SLAs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.SLAId, cancellationToken);

            if (item == null)
                throw new KeyNotFoundException(_localizer["SLA not found."]);

            return new GetSLAByIdResponse
            {
                SLAName = item.SLAName,
                ResponseTimeInHours = item.ResponseTimeInHours,
                ResolutionTimeInHours = item.ResolutionTimeInHours,
                EscalationLevel1Hours = item.EscalationLevel1Hours ?? 0,
                EscalationLevel2Hours = item.EscalationLevel2Hours ?? 0,
                IsActive = item.IsActive,
            };
        }
    }
}
