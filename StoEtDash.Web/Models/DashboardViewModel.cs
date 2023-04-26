namespace StoEtDash.Web.Models
{
	public class DashboardViewModel
	{
		// Widgets
		public double PortfolioValue { get; set; }
		public double InvestedValue { get; set; }
		public double FeesPaid { get; set; }
		public double ExpectedDividends { get; set; }

		// Assets
		public List<AssetViewModel> Assets { get; set; }

		// Charts
		public ChartDataViewModel AssetsByValue { get; set; }
		public ChartDataViewModel AssetsByShares { get; set; }
		public ChartDataViewModel DividendVsOtherChart { get; set; }
		public ChartDataViewModel DailyPricesChart { get; set; }
		public ChartDataViewModel WeeklyPricesChart { get; set; }
		public ChartDataViewModel MonthlyPricesChart { get; set; }
	}
}