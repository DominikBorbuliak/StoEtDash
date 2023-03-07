using Microsoft.AspNetCore.Mvc;

namespace StoEtDash.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
