using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDatabaseService _databaseService;
		private readonly INotyfService _notificationService;

		public DashboardController(IDatabaseService databaseService, INotyfService notificationService)
		{
			_databaseService = databaseService;
			_notificationService = notificationService;
		}

		public IActionResult Index()
		{
			var dashboardViewModel = new DashboardViewModel
			{
				Transactions = _databaseService.GetAllTransactions(HttpContext.Session.GetString("Username") ?? string.Empty)
			};

			return View(dashboardViewModel);
		}

		public IActionResult OnAddTransactionSubmit(TransactionViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", model);
			}

			try
			{
				model.Username = HttpContext.Session.GetString("Username") ?? string.Empty;
				_databaseService.AddTransaction(model);
			}
			catch (UserException exception)
			{
				_notificationService.Error(exception.Message);
				return View("Index", model);
			}

			return RedirectToAction("Index", "Dashboard");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Login");
		}
	}
}
