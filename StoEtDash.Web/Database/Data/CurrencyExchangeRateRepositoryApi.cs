using Newtonsoft.Json;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Data
{
	public class CurrencyExchangeRateRepositoryApi : ICurrencyExchangeRateRepositoryApi
	{
		public const string BaseUrl = "https://currencies.apps.grandtrunk.net";
		public const string ExchangeRateFunctionFormat = "getrate/{0}/{1}/{2}";

		public async Task<double> GetExchangeRateAsync(CurrencyType currencyFrom, CurrencyType currencyTo, DateTime date)
		{
			var queryUrl = string.Format($"{BaseUrl}/{ExchangeRateFunctionFormat}", date.ToString("yyyy-MM-dd"), currencyFrom.ToString(), currencyTo.ToString());
			var queryUri = new Uri(queryUrl);

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync(queryUri);
				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();
				var exchangeRate = JsonConvert.DeserializeObject<double>(responseString);

				return exchangeRate;
			}
		}
	}
}
