using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Infrastructure.Services;

namespace TicketingHub.Api.UnitTests.Infrastructure;

[TestFixture]
public class DBContextFixture
{
    private DBContext _context;
    private CurrentUserService _currentUserService;
    private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    [OneTimeSetUp]
    public void Initialize()
    {
        // Create in-memory database options
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        // Mock IHttpContextAccessor
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var mockHttpContext = new Mock<HttpContext>();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "User")
        };
        var mockClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
        mockHttpContext.Setup(c => c.User).Returns(mockClaimsPrincipal);
        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext.Object);

        // Initialize CurrentUserService and DbContext
        _currentUserService = new CurrentUserService(_mockHttpContextAccessor.Object);
        _context = new DBContext(options, _currentUserService);

        // Ensure database is created
        _context.Database.EnsureCreated();
    }

    public DBContext Context
    {
        get
        {
            if (_context == null)
                throw new InvalidOperationException("Initialize must be called before accessing the context.");
            return _context;
        }
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context?.Dispose();
    }
}
