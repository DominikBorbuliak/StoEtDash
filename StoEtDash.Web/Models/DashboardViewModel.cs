using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Models
{
	public class DashboardViewModel
	{
		public List<Transaction> Transactions { get; set; }
		public double PortfolioValue { get; set; }
		public double InvestedValue { get; set; }
		public double ExpectedDividends { get; set; }
	}
}