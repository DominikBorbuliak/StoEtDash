namespace StoEtDash.Web.Models
{
    public class DashboardViewModel
	{
		public double PortfolioValue { get; set; }
		public double InvestedValue { get; set; }
		public double FeesPaid { get; set; }
		public double ExpectedDividends { get; set; }

		public List<AssetViewModel> Assets { get; set; }
	}
}