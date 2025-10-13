using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;


namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.CreateSLA
{
    public class CreateSLAHandler : IRequestHandler<CreateSLARequest, CreateSLAResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateSLAHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateSLAResponse> Handle(CreateSLARequest request, CancellationToken cancellationToken)
        {
            bool exists = await _context.SLAs
                .AnyAsync(p => p.SLAName.ToLower() == request.SLAName.ToLower(), cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["SLA name already exists."]);

            var item = new SLA
            {
                SLAName = request.SLAName?.Trim(),
                ResponseTimeInHours = request.ResponseTimeInHours,
                ResolutionTimeInHours = request.ResolutionTimeInHours,
                EscalationLevel1Hours = request.EscalationLevel1Hours,
                EscalationLevel2Hours = request.EscalationLevel2Hours,
                IsActive = request.IsActive,
                Id = Guid.NewGuid()
            };

            _context.SLAs.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateSLAResponse
            {
                SLAId = item.Id,
                Message = _localizer["SLA created successfully."]
            };
        }
    }
}
