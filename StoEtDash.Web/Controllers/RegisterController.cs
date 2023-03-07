using Microsoft.AspNetCore.Mvc;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnRegisterSubmit(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Register");
        }
    }
}
