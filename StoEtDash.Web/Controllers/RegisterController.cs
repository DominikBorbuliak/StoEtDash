using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IDatabaseService _databaseService;
		private readonly INotyfService _notificationService;

		public RegisterController(IDatabaseService databaseService, INotyfService notificationService)
		{
			_databaseService = databaseService;
			_notificationService = notificationService;
		}

		/// <summary>
		/// Loads register page view
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Creates user if possible and redirects to main page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public IActionResult OnRegisterSubmit(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", model);
			}

			try
			{
				_databaseService.CreateUser(model);

				HttpContext.Session.SetString("Username", model.Username);
			}
			catch (UserException exception)
			{
				_notificationService.Error(exception.Message);
				return View("Index", model);
			}

			return RedirectToAction("Index", "Dashboard");
		}
	}
}
