using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using TicketingHub.Api;
using TicketingHub.Api.Common.Behaviours;
using TicketingHub.Api.Filters;
using TicketingHub.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("Database");

// Configure DbContext
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DBContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
}
else
{
    builder.Services.AddDbContext<DBContext>(db => db.UseSqlServer(conn), ServiceLifetime.Singleton);
}


builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
builder.Services.AddService();

// Register the DbSeeder class in DI container
builder.Services.AddTransient<DbSeeder>();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>(); // Register the filter globally
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,  // Automatic expiration check
          ClockSkew = TimeSpan.Zero,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
  });

// Add Swagger services
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
       {
           new OpenApiSecurityScheme
           {
               Reference = new OpenApiReference
               {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
               }
           },
           new string[] { }
       }
   });
});

builder.Host.UseSerilog((context, services, configuration) =>
{
    // Read minimum log level from configuration
    var minimumLogLevel = context.Configuration.GetSection("Logging:LogLevel:Default").Value;
    // Convert string to LogEventLevel
    var logLevel = Enum.TryParse<LogEventLevel>(minimumLogLevel, true, out var level) ? level : LogEventLevel.Warning;
    var loggingConfig = context.Configuration.GetSection("LoggingConfiguration");
    var useFileLogging = loggingConfig.GetValue<bool>("UseFileLogging");
    var useDatabaseLogging = loggingConfig.GetValue<bool>("UseDatabaseLogging");

    configuration
        .MinimumLevel.Information()
        .Enrich.FromLogContext();

    if (useFileLogging)
    {
        configuration.WriteTo.File("ApplicationLog/log-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: logLevel);
    }


    if (!builder.Environment.IsDevelopment() && useDatabaseLogging)
    {
        configuration.WriteTo.MSSqlServer(
            connectionString: conn,
            tableName: "ApplicationLog",
            autoCreateSqlTable: true,
            restrictedToMinimumLevel: logLevel
        );
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed the database with initial data
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
        var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
        await dbSeeder.SeedAsync();  // Call the SeedAsync method
    }
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
