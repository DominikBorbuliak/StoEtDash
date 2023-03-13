using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface ICurrencyExchangeRateRepositoryApi
	{
		Task<double> GetExchangeRateAsync(CurrencyType currencyFrom, CurrencyType currencyTo, DateTime date);
	}
}
