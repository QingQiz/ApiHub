using System;
using System.Collections.Generic;
using System.Text;
using ApiHub.Localization;
using Volo.Abp.Application.Services;

namespace ApiHub
{
    /* Inherit your application services from this class.
     */
    public abstract class ApiHubAppService : ApplicationService
    {
        protected ApiHubAppService()
        {
            LocalizationResource = typeof(ApiHubResource);
        }
    }
}
