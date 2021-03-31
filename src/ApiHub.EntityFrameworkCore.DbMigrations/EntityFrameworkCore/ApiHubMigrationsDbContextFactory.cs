using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ApiHub.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ApiHubMigrationsDbContextFactory : IDesignTimeDbContextFactory<ApiHubMigrationsDbContext>
    {
        public ApiHubMigrationsDbContext CreateDbContext(string[] args)
        {
            ApiHubEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ApiHubMigrationsDbContext>()
                .UseSqlite(configuration.GetConnectionString("Default"));

            return new ApiHubMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ApiHub.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
