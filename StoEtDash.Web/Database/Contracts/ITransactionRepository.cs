using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface ITransactionRepository
	{
		void AddTransaction(Transaction transaction);
		List<Transaction> GetAllTransactions(string username);
	}
}
