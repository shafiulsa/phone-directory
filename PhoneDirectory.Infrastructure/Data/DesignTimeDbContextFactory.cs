
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PhoneDirectory.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PhoneDirectoryContext>
    {
        public PhoneDirectoryContext CreateDbContext(string[] args)
        {
            // Adjust path to point to API project where appsettings.json exists
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../PhoneDirectory.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PhoneDirectoryContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new PhoneDirectoryContext(optionsBuilder.Options);
        }
    }
}
