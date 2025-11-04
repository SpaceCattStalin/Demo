using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
            // Trỏ tới appsettings.json của API
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "API");
            var cfg = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var conn = cfg.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseSqlServer(conn)
                .Options;

            return new DemoDbContext(options);
        }
    }
}
