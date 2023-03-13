using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Services
{
	public class DatabaseService : IDatabaseService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITransactionRepository _transactionRepository;

		public DatabaseService(IUserRepository userRepository, ITransactionRepository transactionRepository)
		{
			_userRepository = userRepository;
			_transactionRepository = transactionRepository;
		}

		public User GetUserByUsername(string username) => _userRepository.GetUserByUsername(username);

		public void CreateUser(LoginViewModel loginViewModel) => _userRepository.CreateUser(loginViewModel.ToDatabaseModel());

		public void AddTransaction(TransactionViewModel transactionViewModel) => _transactionRepository.AddTransaction(transactionViewModel.ToDatabaseModel());

		public List<Transaction> GetAllTransactions(string username) => _transactionRepository.GetAllTransactions(username);
	}
}
