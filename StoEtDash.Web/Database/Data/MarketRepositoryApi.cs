using Newtonsoft.Json;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Extensions;

namespace StoEtDash.Web.Database.Data
{
	public class MarketRepositoryApi : IMarketRepositoryApi
	{
		private const string BaseUrl = "https://www.alphavantage.co/query";
		private const string OverviewFunctionFormat = "function=OVERVIEW&symbol={0}&apikey={1}";
		private const string GlobalQuoteFunctionFormat = "function=GLOBAL_QUOTE&symbol={0}&apikey={1}";
		private const string TimeSeriesFunctionFormat = "function={0}&symbol={1}&apikey={2}";
		private readonly string _apiKey;

		public MarketRepositoryApi(string apiKey)
		{
			_apiKey = apiKey;
		}

		public async Task<double> GetDividendPerShareAsync(string ticker)
		{
			var queryUrl = string.Format($"{BaseUrl}?{OverviewFunctionFormat}", ticker, _apiKey);

			var response = await GetMarketingApiResponseAsync(queryUrl);
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

		public async Task<double> GetPricePerShareAsync(string ticker)
		{
			var queryUrl = string.Format($"{BaseUrl}?{GlobalQuoteFunctionFormat}", ticker, _apiKey);

			var response = await GetMarketingApiResponseAsync(queryUrl);
			var responseString = await response.Content.ReadAsStringAsync();
			var globalQuoteResult = JsonConvert.DeserializeObject<GlobalQuoteResultApi>(responseString);

			if (globalQuoteResult?.GlobalQuote == null || !double.TryParse(globalQuoteResult.GlobalQuote.Price, out var pricePerShare))
			{
				throw new UserException("Error occured while gathering data from market repository. Please try again later.");
			}

			return pricePerShare;
		}

		public async Task<Dictionary<DateTime, double>> GetTimeSeriesPricesAsync(TimeSeriesType timeSeriesType, string ticker)
		{
			var queryUrl = string.Format($"{BaseUrl}?{TimeSeriesFunctionFormat}", timeSeriesType.GetTimeSeriesFunctioNname(), ticker, _apiKey);

			var response = await GetMarketingApiResponseAsync(queryUrl);
			var responseString = await response.Content.ReadAsStringAsync();
			var timeSeriesDailyResult = JsonConvert.DeserializeObject<TimeSeriesResultApi>(responseString);

			try
			{
				return timeSeriesType switch
				{
					TimeSeriesType.Daily => timeSeriesDailyResult.TimeSeriesDaily.Take(365).ToDictionary(item => DateTime.Parse(item.Key), item => double.Parse(item.Value.Price)),
					TimeSeriesType.Weekly => timeSeriesDailyResult.TimeSeriesWeekly.Take(52).ToDictionary(item => DateTime.Parse(item.Key), item => double.Parse(item.Value.Price)),
					TimeSeriesType.Monthly => timeSeriesDailyResult.TimeSeriesMonthly.Take(12).ToDictionary(item => DateTime.Parse(item.Key), item => double.Parse(item.Value.Price)),
					_ => throw new ArgumentOutOfRangeException(nameof(timeSeriesType), $"Not expected time series type value: {timeSeriesType}"),
				};
			}
			catch
			{
				throw new UserException("Error occured while gathering data from market repository. Please try again later.");
			}
		}

		/// <summary>
		/// Returns http response message from marketing api based on url
		/// Throws UserException with appropiate message when API returned error or null
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		/// <exception cref="UserException"></exception>
		private static async Task<HttpResponseMessage> GetMarketingApiResponseAsync(string url)
		{
			try
			{
				using var httpClient = new HttpClient();

				var response = await httpClient.GetAsync(new Uri(url));
				response.EnsureSuccessStatusCode();

				return response;
			}
			catch // Once in a while marketing repository throws random errors like 'unable to establish ssl connection etc.'
			{
				throw new UserException("Marketing api is not working. Please try again later. ");
			}
		}
	}
}
