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

		public IActionResult Index()
		{
			return View();
		}

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
