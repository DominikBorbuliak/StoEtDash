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

		public async Task<DashboardViewModel> GetDashboardViewModelAsync(string username)
		{
			var transactions = _transactionRepository.GetAllTransactions(username);

			var assets = transactions
				.GroupBy(transaction => transaction.Ticker)
				.Select(transactionGroup => (
					Ticker: transactionGroup.Key,
					transactionGroup.First().Currency,
					NumberOfShares: transactionGroup.Sum(groupItem => groupItem.NumberOfShares))
				)
				.ToList();

			var expectedDividends = 0.0;
			var portfolioValue = 0.0;
			var investedValue = transactions.Sum(transaction => transaction.TotalInEur);

			foreach (var asset in assets)
			{
				var assetDividendPerShare = await _marketRepositoryApi.GetDividendPerShareAsync(asset.Ticker);
				var assetPricePerShare = await _marketRepositoryApi.GetPricePerShareAsync(asset.Ticker);

				if (assetDividendPerShare != 0 && asset.Currency != CurrencyType.EUR)
				{
					var exchangeRate = await _currencyExchangeRateRepositoryApi.GetExchangeRateAsync(asset.Currency, CurrencyType.EUR, DateTime.Now);
					assetDividendPerShare *= exchangeRate;
				}

				if (assetPricePerShare != 0 && asset.Currency != CurrencyType.EUR)
				{
					var exchangeRate = await _currencyExchangeRateRepositoryApi.GetExchangeRateAsync(asset.Currency, CurrencyType.EUR, DateTime.Now);
					assetPricePerShare *= exchangeRate;
				}

				expectedDividends += assetDividendPerShare * asset.NumberOfShares;
				portfolioValue += assetPricePerShare * asset.NumberOfShares;
			}

			return new DashboardViewModel
			{
				Transactions = transactions,
				ExpectedDividends = expectedDividends,
				PortfolioValue = portfolioValue,
				InvestedValue = investedValue
			};
		}
	}
}
