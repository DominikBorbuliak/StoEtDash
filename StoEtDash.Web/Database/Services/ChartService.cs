using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Services
{
	public class ChartService : IChartService
	{
		private readonly List<string> _colors = new() { "#DDD9FF", "#5D1444", "#BAB2FE", "#8C1E66", "#7465FD", "#BA2887", "#8656E0", "#A938A5", "#9747C2" };

		private readonly IMarketRepositoryApi _marketRepositoryApi;
		private readonly ICurrencyExchangeRateRepositoryApi _currencyExchangeRateRepositoryApi;

		public ChartService(IMarketRepositoryApi marketRepositoryApi, ICurrencyExchangeRateRepositoryApi currencyExchangeRateRepositoryApi)
		{
			_marketRepositoryApi = marketRepositoryApi;
			_currencyExchangeRateRepositoryApi = currencyExchangeRateRepositoryApi;
		}

		public async Task<ChartDataViewModel> GetChartByTypeAsync(List<AssetViewModel> assets, ChartType chartType) => chartType switch
		{
			ChartType.AssetsByValue => GetAssetsByValueChart(assets),
			ChartType.AssetsByShares => GetAssetsBySharesChart(assets),
			ChartType.DividendVsOther => GetDividendVsOtherChart(assets),
			ChartType.DailyPrices => await GetDailyPricesChartAsync(assets),
			ChartType.WeeklyPrices => await GetWeeklyPricesChartAsync(assets),
			ChartType.MonthlyPrices => await GetMonthlyPricesChartAsync(assets),
			_ => throw new ArgumentOutOfRangeException(nameof(chartType), $"Not expected chart type value: {chartType}")
		};

		/// <summary>
		/// Returns assets by value chart
		/// </summary>
		/// <param name="assets"></param>
		/// <returns></returns>
		private ChartDataViewModel GetAssetsByValueChart(List<AssetViewModel> assets)
		{
			var labels = assets
				.Select(asset => asset.Name)
				.ToList();

			var data = assets
				.Select(asset => Math.Round(asset.CurrentPricePerShare * asset.NumberOfShares, 2))
				.ToList();

			return new ChartDataViewModel
			{
				Labels = labels,
				Datasets = new List<ChartDatasetViewModel> {
					new ChartDatasetViewModel {
						Data = data,
						BackgroundColors = GetNColors(data.Count)
					}
				}
			};
		}

		/// <summary>
		/// Returns assets by number of shares chart
		/// </summary>
		/// <param name="assets"></param>
		/// <returns></returns>
		private ChartDataViewModel GetAssetsBySharesChart(List<AssetViewModel> assets)
		{
			var labels = assets
				.Select(asset => asset.Name)
				.ToList();

			var data = assets
				.Select(asset => asset.NumberOfShares)
				.ToList();

			return new ChartDataViewModel
			{
				Labels = labels,
				Datasets = new List<ChartDatasetViewModel> {
					new ChartDatasetViewModel {
						Data = data,
						BackgroundColors = GetNColors(data.Count)
					}
				}
			};
		}

		/// <summary>
		/// Returns dividend assets vs other assets chart
		/// </summary>
		/// <param name="assets"></param>
		/// <returns></returns>
		private ChartDataViewModel GetDividendVsOtherChart(List<AssetViewModel> assets)
		{
			var labels = new List<string> { "Dividend", "Other" };

			var data = assets
				.Aggregate(new double[] { 0, 0 }, (ratio, asset) =>
				{
					if (asset.ExpectedDividends > 0)
					{
						ratio[0] += asset.NumberOfShares;
					}
					else
					{
						ratio[1] += asset.NumberOfShares;
					}

					return ratio;
				})
				.ToList();

			return new ChartDataViewModel
			{
				Labels = labels,
				Datasets = new List<ChartDatasetViewModel> {
					new ChartDatasetViewModel {
						Data = data ,
						BackgroundColors = GetNColors(2)
					}
				}
			};
		}

		private async Task<ChartDataViewModel> GetDailyPricesChartAsync(List<AssetViewModel> assets) => await GetPricesChartAsync(assets, TimeSeriesType.Daily);

		private async Task<ChartDataViewModel> GetWeeklyPricesChartAsync(List<AssetViewModel> assets) => await GetPricesChartAsync(assets, TimeSeriesType.Weekly);

		private async Task<ChartDataViewModel> GetMonthlyPricesChartAsync(List<AssetViewModel> assets) => await GetPricesChartAsync(assets, TimeSeriesType.Monthly);

		/// <summary>
		/// Returns prices chart for assets
		/// </summary>
		/// <param name="assets"></param>
		/// <param name="timeSeriesType"></param>
		/// <returns></returns>
		private async Task<ChartDataViewModel> GetPricesChartAsync(List<AssetViewModel> assets, TimeSeriesType timeSeriesType)
		{
			var tasks = await Task.WhenAll(assets.Select(asset => GetPricesChartAsync(asset.Transactions, timeSeriesType)));

			var longestTimeSeries = tasks
				.MaxBy(task => task.Item1.Count());

			// Check if some stocks/ETFs does not have enough data (stock was created recently)
			var shortTimeSeries = tasks.Where(task => task.Item1.Count() < longestTimeSeries.Item1.Count());
			if (shortTimeSeries.Any())
			{
				foreach (var shortTimeSerie in shortTimeSeries)
				{
					// Prepend 0 to serie if there are missing values
					for (var i = 0; i < longestTimeSeries.Item1.Count() - shortTimeSerie.Item1.Count(); i++)
					{
						shortTimeSerie.Item2.Data?.Prepend(0);
					}
				}
			}

			var labels = longestTimeSeries
				.Item1
				.OrderBy(date => date)
				.Select(date => date.ToString("dd.MM.yyyy"))
				.ToList();

			var datasets = tasks
				.Select(task => task.Item2)
				.ToList();

			var colors = GetNColors(datasets.Count);

			for (var i = 0; i < colors.Count; i++)
			{
				datasets[i].BackgroundColors = new List<string> { colors[i] };
				datasets[i].BorderColor = colors[i];
			}

			return new ChartDataViewModel
			{
				Labels = labels,
				Datasets = datasets
			};
		}

		/// <summary>
		/// Returns prices chart for transactions
		/// </summary>
		/// <param name="transactions"></param>
		/// <returns></returns>
		private async Task<(IEnumerable<DateTime>, ChartDatasetViewModel)> GetPricesChartAsync(List<TransactionViewModel> transactions, TimeSeriesType timeSeriesType)
		{
			var dataset = new ChartDatasetViewModel
			{
				Label = transactions.First().Name,
				Data = new List<double>()
			};

			var prices = await _marketRepositoryApi.GetTimeSeriesPrices(timeSeriesType, transactions.First().Ticker);

			var exchangeRate = 1.0;
			if (transactions.First().Currency != CurrencyType.EUR)
			{
				exchangeRate = await _currencyExchangeRateRepositoryApi.GetExchangeRateAsync(transactions.First().Currency, CurrencyType.EUR, DateTime.Now);
			}

			foreach (var price in prices.OrderBy(item => item.Key))
			{
				// Price is the price at the end of the day, so we can calculate with same day/week/month
				var sharesToDate = transactions
					.Where(transaction => transaction.Time <= price.Key)
					.Aggregate(0.0, (count, transaction) => count + (transaction.ActionType == TransactionActionType.Buy ? transaction.NumberOfShares : -transaction.NumberOfShares));

				dataset.Data.Add(price.Value * sharesToDate * exchangeRate);
			}

			return (prices.Select(item => item.Key), dataset);
		}

		/// <summary>
		/// Returns exactly N colors from preset colors
		/// Loops when there is not enough colors
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		private List<string> GetNColors(int n) => Enumerable.Repeat(_colors, (int)Math.Ceiling((double)n / _colors.Count))
			.SelectMany(color => color)
			.Take(n)
			.ToList();
	}
}
