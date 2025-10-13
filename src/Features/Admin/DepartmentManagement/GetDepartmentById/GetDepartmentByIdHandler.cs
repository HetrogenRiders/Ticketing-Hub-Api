using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.GetDepartmentById
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdRequest, GetDepartmentByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetDepartmentByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetDepartmentByIdResponse> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == request.DepartmentId, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException(_localizer["Department not found."]);

            return new GetDepartmentByIdResponse
            {
                DepartmentId = department.Id,
                DepartmentName = department.DepartmentName,
                ParentDepartmentId = department.ParentDepartmentId,
                HeadUserId = department.HeadUserId,
                IsActive = department.IsActive
            };
        }
    }
}
