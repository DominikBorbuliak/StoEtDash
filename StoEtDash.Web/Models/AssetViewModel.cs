namespace StoEtDash.Web.Models
{
	public class AssetViewModel
	{
		public string Name { get; set; }
		public double NumberOfShares { get; set; }
		public double InvestedValue { get; set; }
		public double FeesPaid { get; set; }
		public double AveragePrice { get; set; }
		public double CurrentPricePerShare { get; set; }
		public double ExpectedDividends { get; set; }
		public List<TransactionViewModel> Transactions { get; set; }
	}
}
