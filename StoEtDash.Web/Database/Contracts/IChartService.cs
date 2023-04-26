using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface IChartService
	{
		/// <summary>
		/// Builds and returns chart by type
		/// </summary>
		/// <param name="assets"></param>
		/// <returns></returns>
		/// <param name="chartType"></param>
		/// <returns></returns>
		Task<ChartDataViewModel> GetChartByTypeAsync(List<AssetViewModel> assets, ChartType chartType);
	}
}
