using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface ICurrencyExchangeRateRepositoryApi
	{
		/// <summary>
		/// Gets the currency rate for specified currencies
		/// </summary>
		/// <param name="currencyFrom"></param>
		/// <param name="currencyTo"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		Task<double> GetExchangeRateAsync(CurrencyType currencyFrom, CurrencyType currencyTo, DateTime date);
	}
}
