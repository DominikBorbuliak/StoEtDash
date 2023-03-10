using Microsoft.AspNetCore.Mvc;

namespace StoEtDash.Web.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
