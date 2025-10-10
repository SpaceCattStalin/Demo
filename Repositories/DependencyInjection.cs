using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Repositories
{
    public static class DependencyInjection
    {
        /// <summary>
        ///  Registers infrastructure-level services such as the database context and ASP.NET Core Identity.
        /// </summary>
        /// <param name="builder">The application host builder, used for configuring services and accessing configuration.</param>
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var logger = LoggerFactory
               .Create(logging =>
               {
                   logging.AddConsole();
                   logging.SetMinimumLevel(LogLevel.Debug);
               })
               .CreateLogger("InfrastructureSetup");

            // Get the connection string from configuration or environment variables
            var connectionString = builder.Configuration["ConnectionString:DefaultConnection"]
                    ?? Environment.GetEnvironmentVariable("ConnectionString:DefaultConnection");

            logger.LogInformation($"--- DATABASE CONNECTION ATTEMPT ---");
            logger.LogDebug($"Connection string: {connectionString}");
            logger.LogInformation($"--- DATABASE CONNECTION ATTEMPT END ---");

            //// Register the ApplicationDbContext with EF Core, using SQL Server as the provider.
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);
            //});

            // Register ASP.NET Core Identity with custom User entity and GUID as the primary key type.
            // Adds EF Core stores for persisting identity data and default token providers for features like password reset.
        }
    }
}
