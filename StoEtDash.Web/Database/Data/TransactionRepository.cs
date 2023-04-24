using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Data
{
	public class TransactionRepository : ITransactionRepository
	{
		public void AddTransaction(Transaction transaction)
		{
			using var dbContext = new StoEtDashContext();

			dbContext.Transactions.Add(transaction);
			dbContext.SaveChanges();
		}

		public void UpdateTransaction(Transaction transaction)
		{
			using var dbContext = new StoEtDashContext();

			dbContext.Transactions.Update(transaction);
			dbContext.SaveChanges();
		}

		public void DeleteTransactionById(string transactionId)
		{
			using var dbContext = new StoEtDashContext();

			var transaction = dbContext.Transactions.First(transaction => transaction.Id.Equals(transactionId));
			dbContext.Transactions.Remove(transaction);

			dbContext.SaveChanges();
		}

		public List<Transaction> GetAllTransactions(string username)
		{
			using var dbContext = new StoEtDashContext();

			return dbContext.Transactions.Where(transaction => transaction.Username.Equals(username)).ToList();
		}

		public List<Transaction> GetTransactionsByTicker(string ticker, string username)
		{
			using var dbContext = new StoEtDashContext();

			return dbContext.Transactions.Where(transaction => transaction.Username.Equals(username) && transaction.Ticker.Equals(ticker)).ToList();
		}

		public Transaction GetTransactionById(string transactionId, string username)
		{
			using var dbContext = new StoEtDashContext();

			return dbContext.Transactions.First(transaction => transaction.Username.Equals(username) && transaction.Id.Equals(transactionId));
		}
	}
}
