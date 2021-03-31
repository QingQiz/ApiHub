using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace ApiHub.EntityFrameworkCore
{
    public static class ApiHubDbContextModelCreatingExtensions
    {
        public static void ConfigureApiHub(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ApiHubConsts.DbTablePrefix + "YourEntities", ApiHubConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}