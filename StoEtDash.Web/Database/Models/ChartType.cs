namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Used to determin what chart needs to be build
	/// </summary>
	public enum ChartType
	{
		AssetsByValue,
		AssetsByShares,
		DividendVsOther,
		DailyPrices,
		WeeklyPrices,
		MonthlyPrices
	}
}
