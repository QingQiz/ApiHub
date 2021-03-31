using System.Collections.Generic;
using System.Globalization;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using ApiHub.Localization;
using ApiHub.Web;
using Volo.Abp.AspNetCore.TestBase;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;

namespace ApiHub
{
    [DependsOn(
        typeof(AbpAspNetCoreTestBaseModule),
        typeof(ApiHubWebModule),
        typeof(ApiHubApplicationTestModule)
    )]
    public class ApiHubWebTestModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<IMvcBuilder>(builder =>
            {
                builder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(ApiHubWebModule).Assembly));
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalizationServices(context.Services);
        }

        private static void ConfigureLocalizationServices(IServiceCollection services)
        {
            var cultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("tr") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ApiHubResource>()
                    .AddBaseTypes(
                        typeof(AbpValidationResource),
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
