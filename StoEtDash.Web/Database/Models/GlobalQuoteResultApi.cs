using Newtonsoft.Json;

namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	///	Model used to get price per share from api
	/// </summary>
	public class GlobalQuoteResultApi
	{
		[JsonProperty("Global Quote")]
		public GlobalQuoteApi GlobalQuote { get; set; }
	}

	/// <summary>
	/// Submodel used to get price per share from api
	/// </summary>
	public class GlobalQuoteApi
	{
		[JsonProperty("05. price")]
		public string Price { get; set; } = string.Empty;
	}
}
