using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.UpdateDepartment
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentRequest, UpdateDepartmentResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateDepartmentHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateDepartmentResponse> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException(_localizer["Department not found."]);

            bool duplicate = await _context.Departments
                .AnyAsync(d => d.DepartmentName.ToLower() == request.DepartmentName.ToLower() && d.Id != request.DepartmentId, cancellationToken);

            if (duplicate)
                throw new InvalidOperationException(_localizer["Another department with this name already exists."]);

            department.DepartmentName = request.DepartmentName.Trim();
            department.ParentDepartmentId = request.ParentDepartmentId;
            department.HeadUserId = request.HeadUserId;
            department.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateDepartmentResponse
            {
                Message = _localizer["Department updated successfully."]
            };
        }
    }
}
