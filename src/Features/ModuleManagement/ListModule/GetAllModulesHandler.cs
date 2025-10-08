using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.ModuleManagement;
public sealed class GetAllModulesHandler : IRequestHandler<GetAllModulesRequest, GetAllModulesResponse>
{
    private readonly DBContext _context;

    public GetAllModulesHandler(DBContext context)
    {
        _context = context;
    }

    public async Task<GetAllModulesResponse> Handle(GetAllModulesRequest request, CancellationToken cancellationToken)
    {
        // modules are hardcoded in BDSeeder.cs file

        var modules = await _context.Modules.ToListAsync(cancellationToken);

        return new GetAllModulesResponse
        {
            Modules = modules
        };
    }
}
