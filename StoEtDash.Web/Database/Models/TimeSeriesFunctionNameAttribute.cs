namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Attribute to represent the name of time series function
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class TimeSeriesFunctionNameAttribute : Attribute
	{
		public string TimeSeriesFunctionName { get; protected set; }

		public TimeSeriesFunctionNameAttribute(string timeSeriesFunctionName)
		{
			TimeSeriesFunctionName = timeSeriesFunctionName;
		}
	}
}
