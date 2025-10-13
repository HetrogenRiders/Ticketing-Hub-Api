using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.CreateStatus
{
    public class CreateStatusHandler : IRequestHandler<CreateStatusRequest, CreateStatusResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateStatusHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateStatusResponse> Handle(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            bool exists = await _context.Statuses
                .AnyAsync(p => p.StatusName.ToLower() == request.StatusName.ToLower(), cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["Status name already exists."]);

            var item = new Status
            {
                StatusName = request.StatusName?.Trim(),
                SortOrder = request.SortOrder,
                ColorCode = request.ColorCode,
                IsFinalStatus = request.IsFinalStatus,
                IsActive = request.IsActive,
                Id = Guid.NewGuid()
            };

            _context.Statuses.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateStatusResponse
            {
                StatusId = item.Id,
                Message = _localizer["Status created successfully."]
            };
        }
    }
}
