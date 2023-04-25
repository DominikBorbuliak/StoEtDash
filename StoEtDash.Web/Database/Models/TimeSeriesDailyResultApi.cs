using Newtonsoft.Json;

namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Model used to gather daily prices
	/// </summary>
	public class TimeSeriesDailyResultApi
	{
		[JsonProperty("Time Series (Daily)")]
		public Dictionary<string, TimeSerieDailyApi> TimeSeriesDaily { get; set; }
	}

	/// <summary>
	/// Model used to gather daily prices
	/// </summary>
	public class TimeSerieDailyApi
	{
		[JsonProperty("4. close")]
		public string Price { get; set; }
	}
}
