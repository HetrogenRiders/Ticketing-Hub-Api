using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetAllSLA
{
    public class GetAllSLAHandler : IRequestHandler<GetAllSLARequest, GetAllSLAResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetAllSLAHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetAllSLAResponse> Handle(GetAllSLARequest request, CancellationToken cancellationToken)
        {
            var items = await _context.SLAs
                .AsNoTracking()
                .OrderBy(x => x.SLAName)
                .Select(x => new SLADto { Id = x.Id, SLAName = x.SLAName, IsActive = x.IsActive })
                .ToListAsync(cancellationToken);

            return new GetAllSLAResponse { SLADtos = items };
        }
    }
}
