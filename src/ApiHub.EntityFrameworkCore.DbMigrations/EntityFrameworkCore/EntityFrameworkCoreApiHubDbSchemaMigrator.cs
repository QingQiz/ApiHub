using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiHub.Data;
using Volo.Abp.DependencyInjection;

namespace ApiHub.EntityFrameworkCore
{
    public class EntityFrameworkCoreApiHubDbSchemaMigrator
        : IApiHubDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreApiHubDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ApiHubMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ApiHubMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}