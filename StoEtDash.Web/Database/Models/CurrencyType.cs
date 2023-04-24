namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// List of available currencies in the app
	/// </summary>
	public enum CurrencyType
	{
		[CurrencyDisplayFormat("{0} €")]
		EUR,

		[CurrencyDisplayFormat("${0}")]
		USD
	}
}
