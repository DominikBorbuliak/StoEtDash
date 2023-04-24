using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Services
{
	public class DatabaseService : IDatabaseService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IMarketRepositoryApi _marketRepositoryApi;
		private readonly ICurrencyExchangeRateRepositoryApi _currencyExchangeRateRepositoryApi;

		public DatabaseService(IUserRepository userRepository, ITransactionRepository transactionRepository, IMarketRepositoryApi marketRepositoryApi, ICurrencyExchangeRateRepositoryApi currencyExchangeRateRepositoryApi)
		{
			_userRepository = userRepository;
			_transactionRepository = transactionRepository;
			_marketRepositoryApi = marketRepositoryApi;
			_currencyExchangeRateRepositoryApi = currencyExchangeRateRepositoryApi;
		}

		public User GetUserByUsername(string username) => _userRepository.GetUserByUsername(username);

		public void CreateUser(LoginViewModel loginViewModel) => _userRepository.CreateUser(loginViewModel.ToDatabaseModel());

		public void AddTransaction(TransactionViewModel transactionViewModel) => _transactionRepository.AddTransaction(transactionViewModel.ToDatabaseModel());

		public void UpdateTransaction(TransactionViewModel transactionViewModel) => _transactionRepository.UpdateTransaction(transactionViewModel.ToDatabaseModel());

		public void DeleteTransactionById(string transactionId) => _transactionRepository.DeleteTransactionById(transactionId);

		public List<TransactionViewModel> GetTransactionsByTicker(string ticker, string username) => _transactionRepository.GetTransactionsByTicker(ticker, username)
			.Select(transaction => transaction.FromDatabaseModel()).ToList();

		public TransactionViewModel GetTransactionById(string transactionId, string username) => _transactionRepository.GetTransactionById(transactionId, username).FromDatabaseModel();

		public async Task<DashboardViewModel> GetDashboardViewModelAsync(string username)
		{
			var transactions = _transactionRepository.GetAllTransactions(username);

			var transactionGroups = transactions.GroupBy(transaction => transaction.Ticker);

			var assets = await Task.WhenAll(transactionGroups.Select(group => GetAssetViewModel(group.Key, group.ToList())));

			return new DashboardViewModel
			{
				PortfolioValue = assets.Sum(asset => asset.CurrentPricePerShare * asset.NumberOfShares),
				InvestedValue = assets.Sum(asset => asset.InvestedValue),
				FeesPaid = assets.Sum(asset => asset.FeesPaid),
				ExpectedDividends = assets.Sum(asset => asset.ExpectedDividends),
				Assets = assets.ToList()
			};
		}

		/// <summary>
		/// Returns asset view model with all transactions and computed values
		/// </summary>
		/// <param name="ticker"></param>
		/// <param name="transactions"></param>
		/// <returns></returns>
		private async Task<AssetViewModel> GetAssetViewModel(string ticker, List<Transaction> transactions)
		{
			var currency = transactions.First().Currency;

			var asset = new AssetViewModel
			{
				Name = transactions.First().Name,
				CurrentPricePerShare = await _marketRepositoryApi.GetPricePerShareAsync(ticker),
				ExpectedDividends = await _marketRepositoryApi.GetDividendPerShareAsync(ticker),
				Transactions = transactions.Select(transaction => transaction.FromDatabaseModel()).ToList()
			};

			if (asset.CurrentPricePerShare != 0 && currency != CurrencyType.EUR)
			{
				var exchangeRate = await _currencyExchangeRateRepositoryApi.GetExchangeRateAsync(currency, CurrencyType.EUR, DateTime.Now);
				asset.CurrentPricePerShare *= exchangeRate;
			}

			if (asset.ExpectedDividends != 0 && currency != CurrencyType.EUR)
			{
				var exchangeRate = await _currencyExchangeRateRepositoryApi.GetExchangeRateAsync(currency, CurrencyType.EUR, DateTime.Now);
				asset.ExpectedDividends *= exchangeRate;
			}

			foreach (var transaction in transactions.OrderBy(transaction => transaction.Time))
			{
				if (transaction.Action == TransactionActionType.Buy)
				{
					asset.NumberOfShares += transaction.NumberOfShares;
					asset.InvestedValue += transaction.TotalInEur - transaction.FeesInEur;
					asset.AveragePrice = asset.InvestedValue / asset.NumberOfShares;
					asset.FeesPaid += transaction.FeesInEur;
				}
				else
				{
					asset.NumberOfShares -= transaction.NumberOfShares;
					asset.InvestedValue -= asset.AveragePrice * transaction.NumberOfShares;
					asset.FeesPaid += transaction.FeesInEur;
				}
			}

			asset.ExpectedDividends *= asset.NumberOfShares;

			return asset;
		}
	}
}
