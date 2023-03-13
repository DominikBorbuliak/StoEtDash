using Newtonsoft.Json;

namespace StoEtDash.Web.Database.Models
{
	public class GlobalQuoteResultApi
	{
		[JsonProperty("Global Quote")]
		public GlobalQuoteApi GlobalQuote { get; set; }
	}

	public class GlobalQuoteApi
	{
		[JsonProperty("05. price")]
		public string Price { get; set; } = string.Empty;
	}
}
