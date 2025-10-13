using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.CreateDepartment
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentRequest, CreateDepartmentResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateDepartmentHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateDepartmentResponse> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var existing = await _context.Departments
                .AnyAsync(d => d.DepartmentName.ToLower() == request.DepartmentName.ToLower() && d.IsActive, cancellationToken);

            if (existing)
                throw new InvalidOperationException(_localizer["Department name already exists."]);

            var department = new Department
            {
                Id = Guid.NewGuid(),
                DepartmentName = request.DepartmentName.Trim(),
                ParentDepartmentId = request.ParentDepartmentId,
                HeadUserId = request.HeadUserId,
                IsActive = request.IsActive
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateDepartmentResponse
            {
                DepartmentId = department.Id,
                Message = _localizer["Department created successfully."]
            };
        }
    }
}
