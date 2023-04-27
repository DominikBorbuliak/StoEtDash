using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface IDatabaseService
	{
		/// <summary>
		/// Finds user by username
		/// Throws UserException with appropiate text when user not found
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		User GetUserByUsername(string username);

		/// <summary>
		/// Adds user to the database
		/// </summary>
		/// <param name="loginViewModel"></param>
		void CreateUser(LoginViewModel loginViewModel);

		/// <summary>
		/// Adds transaction to the database
		/// </summary>
		/// <param name="transactionViewModel"></param>
		void AddTransaction(TransactionViewModel transactionViewModel);

		/// <summary>
		/// Updates transaction in the database
		/// </summary>
		/// <param name="transactionViewModel"></param>
		void UpdateTransaction(TransactionViewModel transactionViewModel);

		/// <summary>
		/// Deletes transaction with specified id in database
		/// Throws UserException with appropiate text when transaction could not be deleted
		/// </summary>
		/// <param name="transactionId"></param>
		void DeleteTransactionById(string transactionId);

		/// <summary>
		/// Gets list of user transactions with specified ticker
		/// </summary>
		/// <param name="ticker"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		List<TransactionViewModel> GetTransactionsByTicker(string ticker, string username);

		/// <summary>
		/// Gets user transaction with specified id
		/// Returns null if transaction does not exists
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		TransactionViewModel? GetTransactionById(string transactionId, string username);

		/// <summary>
		/// Gets main page view model with all calculated values,
		/// list of assets and list of transactions for each asset
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		Task<DashboardViewModel> GetDashboardViewModelAsync(string username);
	}
}
