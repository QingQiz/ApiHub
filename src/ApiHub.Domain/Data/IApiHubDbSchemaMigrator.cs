using System.Threading.Tasks;

namespace ApiHub.Data
{
    public interface IApiHubDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
