using StoEtDash.Web.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace StoEtDash.Web.Models
{
	public class TransactionViewModel
	{
		public TransactionActionType ActionType { get; set; }
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
		[DataType(DataType.DateTime)]
		public DateTime Time { get; set; } = DateTime.Now;
		public string Ticker { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public double NumberOfShares { get; set; }
		public double PricePerShare { get; set; }
		public CurrencyType Currency { get; set; }
		public double ExchangeRate { get; set; }
		public double TotalInEur { get; set; }
		public double FeesInEur { get; set; }
		public string Username { get; set; } = string.Empty;
	}

	public static class TransactionViewModelMapper
	{
		public static Transaction ToDatabaseModel(this TransactionViewModel transactionViewModel) => new()
		{
			Action = transactionViewModel.ActionType,
			Time = transactionViewModel.Time,
			Ticker = transactionViewModel.Ticker,
			Name = transactionViewModel.Name,
			NumberOfShares = transactionViewModel.NumberOfShares,
			PricePerShare = transactionViewModel.PricePerShare,
			Currency = transactionViewModel.Currency,
			ExchangeRate = transactionViewModel.ExchangeRate,
			TotalInEur = transactionViewModel.TotalInEur,
			FeesInEur = transactionViewModel.FeesInEur,
			Username = transactionViewModel.Username
		};
	}
}
