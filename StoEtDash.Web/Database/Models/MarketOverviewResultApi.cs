using Newtonsoft.Json;

namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Model used to gather expected dividend per share from api
	/// </summary>
	public class MarketOverviewResultApi
	{
		[JsonProperty("DividendPerShare")]
		public string DividendPerShare { get; set; } = string.Empty;
	}
}
