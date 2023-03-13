using Newtonsoft.Json;

namespace StoEtDash.Web.Database.Models
{
	public class MarketOverviewResultApi
	{
		[JsonProperty("DividendPerShare")]
		public string DividendPerShare { get; set; } = string.Empty;
	}
}
