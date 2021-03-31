using ApiHub.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ApiHub.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ApiHubEntityFrameworkCoreDbMigrationsModule),
        typeof(ApiHubApplicationContractsModule)
        )]
    public class ApiHubDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
