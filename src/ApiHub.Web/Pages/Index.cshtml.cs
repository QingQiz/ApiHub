using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApiHub.Web.Pages
{
    public class Index : PageModel
    {
        public IActionResult OnGet()
        {
            return Redirect("/swagger");
        }
    }
}