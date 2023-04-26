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

			// Check if number of shares would not be less than 0 if transaction was deleted
			var transactionsToCheck = dbContext.Transactions
				.Where(t => t.Username.Equals(transaction.Username) && t.Ticker.Equals(transaction.Ticker) && !t.Id.Equals(transactionId))
				.OrderBy(t => t.Time);

			var numberOfShares = 0.0;
			foreach (var transactionToCheck in transactionsToCheck)
			{
				numberOfShares += transactionToCheck.Action == TransactionActionType.Buy ? transactionToCheck.NumberOfShares : -transactionToCheck.NumberOfShares;

				if (numberOfShares < 0)
				{
					throw new UserException("Selected transaction can not be deleted, because number of shares would be less than 0.");
				}
			}

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
