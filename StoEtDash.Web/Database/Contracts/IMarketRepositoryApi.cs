namespace StoEtDash.Web.Database.Contracts
{
	public interface IMarketRepositoryApi
	{
		/// <summary>
		/// Gets expected dividend per share by ticket
		/// Throws user exception with appropiate text when error occurs
		/// </summary>
		/// <param name="ticker"></param>
		/// <returns></returns>
		Task<double> GetDividendPerShareAsync(string ticker);

		/// <summary>
		/// Gets end of the day price per share by ticker
		/// Throws user exception with appropiate text when error occurs
		/// </summary>
		/// <param name="ticker"></param>
		/// <returns></returns>
		Task<double> GetPricePerShareAsync(string ticker);

		/// <summary>
		/// Gets previous daily prices for ticker
		/// Throws user exception with appropiate text when error occurs
		/// </summary>
		/// <param name="ticker"></param>
		/// <returns></returns>
		Task<Dictionary<DateTime, double>> GetDailyPricesAsync(string ticker);
	}
}
