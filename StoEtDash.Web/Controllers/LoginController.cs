using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Extensions;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Controllers
{
	public class LoginController : Controller
	{
		private readonly IDatabaseService _databaseService;
		private readonly INotyfService _notificationService;

		public LoginController(IDatabaseService databaseService, INotyfService notificationService)
		{
			_databaseService = databaseService;
			_notificationService = notificationService;
		}

		/// <summary>
		/// Loads login page view
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Checks whether user exists and compares passwords
		/// If user authorized, redirects to main page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public IActionResult OnLoginSubmit(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", model);
			}

			try
			{
				var user = _databaseService.GetUserByUsername(model.Username);

				if (!model.Password.ToSha512().Equals(user.Password))
				{
					_notificationService.Error("Invalid password.");
					return View("Index", model);
				}

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
