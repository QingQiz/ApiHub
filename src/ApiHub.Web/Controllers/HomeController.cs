using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace ApiHub.Web.Controllers
{
    public class HomeController : AbpController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}