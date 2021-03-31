using ApiHub.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ApiHub.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ApiHubController : AbpController
    {
        protected ApiHubController()
        {
            LocalizationResource = typeof(ApiHubResource);
        }
    }
}