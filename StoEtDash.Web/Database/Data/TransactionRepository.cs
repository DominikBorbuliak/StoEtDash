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

		public List<Transaction> GetAllTransactions(string username)
		{
			using var dbContext = new StoEtDashContext();

			return dbContext.Transactions.Where(transaction => transaction.Username.Equals(username)).ToList();
		}
	}
}
