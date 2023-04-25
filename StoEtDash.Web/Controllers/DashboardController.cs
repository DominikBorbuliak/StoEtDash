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

		/// <summary>
		/// Loads main page view
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Index()
		{
			var username = HttpContext.Session.GetString("Username") ?? string.Empty;

			try
			{
				var dashboardViewModel = await _databaseService.GetDashboardViewModelAsync(username);
				return View(dashboardViewModel);
			}
			catch (UserException exception)
			{
				_notificationService.Error(exception.Message);
				return RedirectToAction("Index", "Login");
			}
		}

		/// <summary>
		/// Adds buy transaction to database and redirects to main page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public IActionResult OnBuyTransactionModalSubmit(TransactionViewModel model)
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

		/// <summary>
		/// Adds sell transaction to database and redirects to main page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public IActionResult OnSellTransactionModalSubmit(TransactionViewModel model) => OnBuyTransactionModalSubmit(model);

		/// <summary>
		/// Clears user session and redirects to login page
		/// </summary>
		/// <returns></returns>
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Login");
		}

		/// <summary>
		/// Returns transaction list as html
		/// </summary>
		/// <param name="ticker"></param>
		/// <returns></returns>
		public IActionResult ShowAssetTransactions(string ticker)
		{
			var username = HttpContext.Session.GetString("Username") ?? string.Empty;
			var transactions = _databaseService.GetTransactionsByTicker(ticker, username);

			return PartialView("_TransactionList", transactions);
		}

		/// <summary>
		/// Returns transaction edit modal as html
		/// </summary>
		/// <param name="transactionId"></param>
		/// <returns></returns>
		public IActionResult ShowEditTransactionModal(string transactionId)
		{
			var username = HttpContext.Session.GetString("Username") ?? string.Empty;
			var transaction = _databaseService.GetTransactionById(transactionId, username);

			return PartialView("_EditTransactionModalBody", transaction);
		}

		/// <summary>
		/// Saves edited transaction and redirects to main page
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public IActionResult OnEditTransactionModalSubmit(TransactionViewModel transaction)
		{
			transaction.Username = HttpContext.Session.GetString("Username") ?? string.Empty;
			_databaseService.UpdateTransaction(transaction);

			return RedirectToAction("Index", "Dashboard");
		}

		/// <summary>
		/// Deletes transaction from database and redirects to main page
		/// </summary>
		/// <param name="transactionId"></param>
		/// <returns></returns>
		public IActionResult OnDeleteTransactionClicked(string transactionId)
		{
			_databaseService.DeleteTransactionById(transactionId);

			return RedirectToAction("Index", "Dashboard");
		}
	}
}
