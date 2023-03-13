using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using System.Text.Json;

namespace StoEtDash.Web.Database.Data
{
	public class MarketRepositoryApi : IMarketRepositoryApi
	{
		private const string BaseUrl = "https://www.alphavantage.co/query";
		private const string OverviewFunctionFormat = "function=OVERVIEW&symbol={0}&apikey={1}";
		private const string GlobalQuoteFunctionFormat = "function=GLOBAL_QUOTE&symbol={0}&apikey={1}";

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
				var marketOverviewResult = JsonSerializer.Deserialize<MarketOverviewResultApi>(responseString);

				if (marketOverviewResult == null || !double.TryParse(marketOverviewResult.DividendPerShare, out var dividendPerShare))
				{
					return 0;
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
				var globalQuoteResult = JsonSerializer.Deserialize<GlobalQuoteResultApi>(responseString);

				if (globalQuoteResult?.GlobalQuote == null || !double.TryParse(globalQuoteResult.GlobalQuote.Price, out var pricePerShare))
				{
					return 0;
				}

				return pricePerShare;
			}
		}
	}
}
