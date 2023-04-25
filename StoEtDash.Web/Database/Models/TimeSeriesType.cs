namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Determines which function endpoint will be used in marketing api
	/// </summary>
	public enum TimeSeriesType
	{
		[TimeSeriesFunctionName("TIME_SERIES_DAILY")]
		Daily,

		[TimeSeriesFunctionName("TIME_SERIES_WEEKLY")]
		Weekly,

		[TimeSeriesFunctionName("TIME_SERIES_MONTHLY")]
		Monthly
	}
}
