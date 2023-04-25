using Newtonsoft.Json;

namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Model used to gather daily prices
	/// </summary>
	public class TimeSeriesResultApi
	{
		[JsonProperty("Time Series (Daily)")]
		public Dictionary<string, TimeSeriePriceApi> TimeSeriesDaily { get; set; }


		[JsonProperty("Weekly Time Series")]
		public Dictionary<string, TimeSeriePriceApi> TimeSeriesWeekly { get; set; }


		[JsonProperty("Monthly Time Series")]
		public Dictionary<string, TimeSeriePriceApi> TimeSeriesMonthly { get; set; }
	}

	/// <summary>
	/// Model used to gather daily prices
	/// </summary>
	public class TimeSeriePriceApi
	{
		[JsonProperty("4. close")]
		public string Price { get; set; }
	}
}
