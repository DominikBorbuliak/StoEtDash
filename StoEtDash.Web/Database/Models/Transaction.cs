using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoEtDash.Web.Database.Models
{
	public class Transaction
	{
		[Key]
		[Column(TypeName = "VARCHAR (36)")]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[Column(TypeName = "VARCHAR (4)")]
		public TransactionActionType Action { get; set; }

		[Column(TypeName = "DATETIME")]
		public DateTime Time { get; set; }

		[Column(TypeName = "VARCHAR (10)")]
		public string Ticker { get; set; } = string.Empty;

		[Column(TypeName = "VARCHAR (75)")]
		public string Name { get; set; } = string.Empty;

		[Column(TypeName = "DOUBLE")]
		public double NumberOfShares { get; set; }

		[Column(TypeName = "DOUBLE")]
		public double PricePerShare { get; set; }

		[Column(TypeName = "VARCHAR (3)")]
		public CurrencyType Currency { get; set; }

		[Column(TypeName = "DOUBLE")]
		public double ExchangeRate { get; set; }

		[Column(TypeName = "DOUBLE")]
		public double TotalInEur { get; set; }

		[Column(TypeName = "DOUBLE")]
		public double FeesInEur { get; set; }

		[ForeignKey("User")]
		[Column(TypeName = "VARCHAR (60)")]
		public string Username { get; set; } = string.Empty;
		public User User { get; set; }
	}
}
