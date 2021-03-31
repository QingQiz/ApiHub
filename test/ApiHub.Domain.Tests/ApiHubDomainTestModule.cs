using ApiHub.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ApiHub
{
    [DependsOn(
        typeof(ApiHubEntityFrameworkCoreTestModule)
        )]
    public class ApiHubDomainTestModule : AbpModule
    {

    }
}