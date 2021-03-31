using Volo.Abp.Modularity;

namespace ApiHub
{
    [DependsOn(
        typeof(ApiHubApplicationModule),
        typeof(ApiHubDomainTestModule)
        )]
    public class ApiHubApplicationTestModule : AbpModule
    {

    }
}