namespace StoEtDash.Web.Database.Contracts
{
	public interface IMarketRepositoryApi
	{
		Task<double> GetDividendPerShareAsync(string ticker);
		Task<double> GetPricePerShareAsync(string ticker);
	}
}
