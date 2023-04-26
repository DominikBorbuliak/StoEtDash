namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Attribute to represent the display format of currency
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class CurrencyDisplayFormatAttribute : Attribute
	{
		public string CurrencyDisplayFormat { get; protected set; }

		public CurrencyDisplayFormatAttribute(string currencyDisplayFormat)
		{
			CurrencyDisplayFormat = currencyDisplayFormat;
		}
	}
}
