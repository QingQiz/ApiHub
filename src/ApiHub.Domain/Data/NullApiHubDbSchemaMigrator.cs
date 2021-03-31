using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ApiHub.Data
{
    /* This is used if database provider does't define
     * IApiHubDbSchemaMigrator implementation.
     */
    public class NullApiHubDbSchemaMigrator : IApiHubDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}