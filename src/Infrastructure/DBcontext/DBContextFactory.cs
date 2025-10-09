using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TicketingHub.Api.Infrastructure
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Database"));

            // Use a fake ICurrentUserService for design-time
            var currentUserService = new FakeCurrentUserService();

            return new DBContext(optionsBuilder.Options, currentUserService);
        }

        private class FakeCurrentUserService : TicketingHub.Api.Common.Interfaces.ICurrentUserService
        {
            public string? UserId => "DesignTime";

            public List<string> Roles => throw new NotImplementedException();
        }
    }
}
