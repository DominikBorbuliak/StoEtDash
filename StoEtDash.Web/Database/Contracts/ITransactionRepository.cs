using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface ITransactionRepository
	{
		/// <summary>
		/// Adds transaction to the database
		/// </summary>
		/// <param name="transaction"></param>
		void AddTransaction(Transaction transaction);

		/// <summary>
		/// Updates transaction in the database
		/// </summary>
		/// <param name="transaction"></param>
		void UpdateTransaction(Transaction transaction);

		/// <summary>
		/// Deletes transaction by id
		/// Expects that transaction id exists
		/// Throws UserException with appropiate text when transaction could not be deleted
		/// </summary>
		/// <param name="transactionId"></param>
		void DeleteTransactionById(string transactionId);

		/// <summary>
		/// Returns list of all user transactions
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		List<Transaction> GetAllTransactions(string username);

		/// <summary>
		/// Returns list of user transactions with specified ticker
		/// </summary>
		/// <param name="ticker"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		List<Transaction> GetTransactionsByTicker(string ticker, string username);

		/// <summary>
		/// Returns user transaction with specified id
		/// Returns null if transaction does not exists
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		Transaction? GetTransactionById(string transactionId, string username);
	}
}
