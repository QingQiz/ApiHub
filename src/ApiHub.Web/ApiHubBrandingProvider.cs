using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ApiHub.Web
{
    [Dependency(ReplaceServices = true)]
    public class ApiHubBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "ApiHub";
    }
}
