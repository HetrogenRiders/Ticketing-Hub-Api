using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetByIdPriority
{
    public class GetPriorityByIdHandler : IRequestHandler<GetPriorityByIdRequest, GetPriorityByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetPriorityByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetPriorityByIdResponse> Handle(GetPriorityByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.Priorities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.PriorityId, cancellationToken);

            if (item == null)
                throw new KeyNotFoundException(_localizer["Priority not found."]);

            return new GetPriorityByIdResponse
            {
                PriorityName = item.PriorityName,
                SLAHours = item.SLAHours,
                ColorCode = item.ColorCode,
                IsActive = item.IsActive,
            };
        }
    }
}
