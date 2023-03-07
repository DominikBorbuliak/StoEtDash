using Microsoft.AspNetCore.Mvc;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnLoginSubmit(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Register");
        }
    }
}
