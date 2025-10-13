using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetByIdStatus
{
    public class GetStatusByIdHandler : IRequestHandler<GetStatusByIdRequest, GetStatusByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetStatusByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetStatusByIdResponse> Handle(GetStatusByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.Statuses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.StatusId, cancellationToken);

            if (item == null)
                throw new KeyNotFoundException(_localizer["Status not found."]);

            return new GetStatusByIdResponse
            {
                StatusName = item.StatusName,
                SortOrder = item.SortOrder,
                ColorCode = item.ColorCode,
                IsFinalStatus = item.IsFinalStatus,
                IsActive = item.IsActive,
            };
        }
    }
}
