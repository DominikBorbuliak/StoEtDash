namespace StoEtDash.Web.Database.Contracts
{
	public interface IMarketRepositoryApi
	{
		/// <summary>
		/// Gets expected dividend per share by ticket
		/// </summary>
		/// <param name="ticker"></param>
		/// <returns></returns>
		Task<double> GetDividendPerShareAsync(string ticker);

		/// <summary>
		/// Gets end of the day price per share by ticker
		/// </summary>
		/// <param name="ticker"></param>
		/// <returns></returns>
		Task<double> GetPricePerShareAsync(string ticker);
	}
}
