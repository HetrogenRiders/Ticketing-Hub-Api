using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;


namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.CreatePriority
{
    public class CreatePriorityHandler : IRequestHandler<CreatePriorityRequest, CreatePriorityResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreatePriorityHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreatePriorityResponse> Handle(CreatePriorityRequest request, CancellationToken cancellationToken)
        {
            bool exists = await _context.Priorities
                .AnyAsync(p => p.PriorityName.ToLower() == request.PriorityName.ToLower(), cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["Priority name already exists."]);

            var item = new Priority
            {
                PriorityName = request.PriorityName?.Trim(),
                SLAHours = request.SLAHours,
                ColorCode = request.ColorCode,
                IsActive = request.IsActive,
                Id = Guid.NewGuid()
            };

            _context.Priorities.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreatePriorityResponse
            {
                PriorityId = item.Id,
                Message = _localizer["Priority created successfully."]
            };
        }
    }
}
