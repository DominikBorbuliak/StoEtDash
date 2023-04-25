using Newtonsoft.Json;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Data
{
	public class MarketRepositoryApi : IMarketRepositoryApi
	{
		private const string BaseUrl = "https://www.alphavantage.co/query";
		private const string OverviewFunctionFormat = "function=OVERVIEW&symbol={0}&apikey={1}";
		private const string GlobalQuoteFunctionFormat = "function=GLOBAL_QUOTE&symbol={0}&apikey={1}";
		private const string TimeSeriesDailyFunctionFormat = "function=TIME_SERIES_DAILY&symbol={0}&apikey={1}";
		private readonly string _apiKey;

		public MarketRepositoryApi(string apiKey)
		{
			_apiKey = apiKey;
		}

		public async Task<double> GetDividendPerShareAsync(string ticker)
		{
			var queryUrl = string.Format($"{BaseUrl}?{OverviewFunctionFormat}", ticker, _apiKey);
			var queryUri = new Uri(queryUrl);

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync(queryUri);
				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();
				var marketOverviewResult = JsonConvert.DeserializeObject<MarketOverviewResultApi>(responseString);

				// ETFs do not have dividend per share so its ok when it is null
				if (marketOverviewResult != null && string.IsNullOrEmpty(marketOverviewResult.DividendPerShare))
				{
					return 0;
				}

				if (marketOverviewResult == null || !double.TryParse(marketOverviewResult.DividendPerShare, out var dividendPerShare))
				{
					throw new UserException("Error occured while gathering data from market repository. Please try again later.");
				}

				return dividendPerShare;
			}
		}

		public async Task<double> GetPricePerShareAsync(string ticker)
		{
			var queryUrl = string.Format($"{BaseUrl}?{GlobalQuoteFunctionFormat}", ticker, _apiKey);
			var queryUri = new Uri(queryUrl);

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync(queryUri);
				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();
				var globalQuoteResult = JsonConvert.DeserializeObject<GlobalQuoteResultApi>(responseString);

				if (globalQuoteResult?.GlobalQuote == null || !double.TryParse(globalQuoteResult.GlobalQuote.Price, out var pricePerShare))
				{
					throw new UserException("Error occured while gathering data from market repository. Please try again later.");
				}

				return pricePerShare;
			}
		}

		public async Task<Dictionary<DateTime, double>> GetDailyPricesAsync(string ticker)
		{
			var queryUrl = string.Format($"{BaseUrl}?{TimeSeriesDailyFunctionFormat}", ticker, _apiKey);
			var queryUri = new Uri(queryUrl);

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync(queryUri);
				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();
				var timeSeriesDailyResult = JsonConvert.DeserializeObject<TimeSeriesDailyResultApi>(responseString);

				try
				{
					return timeSeriesDailyResult.TimeSeriesDaily.ToDictionary(item => DateTime.Parse(item.Key), item => double.Parse(item.Value.Price));
				}
				catch
				{
					throw new UserException("Error occured while gathering data from market repository. Please try again later.");
				}
			}
		}
	}
}
