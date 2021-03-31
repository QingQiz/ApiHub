using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ApiHub.EntityFrameworkCore
{
    [DependsOn(
        typeof(ApiHubEntityFrameworkCoreModule)
        )]
    public class ApiHubEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ApiHubMigrationsDbContext>();
        }
    }
}
