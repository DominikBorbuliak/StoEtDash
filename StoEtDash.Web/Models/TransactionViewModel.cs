using StoEtDash.Web.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace StoEtDash.Web.Models
{
	public class TransactionViewModel
	{
		public string? Id { get; set; }
		public TransactionActionType ActionType { get; set; }

		[Required(ErrorMessage = "Please enter the date and time.")]
		[DataType(DataType.DateTime)]
		public DateTime Time { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Please use the search to fill ticker.")]
		[DataType(DataType.Text)]
		public string Ticker { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please use the search to fill name.")]
		[DataType(DataType.Text)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter the number of shares.")]
		[Range(0.00000001, int.MaxValue, ErrorMessage = "Please enter the number greater or equal to 0.00000001.")]
		public double NumberOfShares { get; set; }

		[Required(ErrorMessage = "Please enter the price per share.")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter the number greater or equal to 0.")]
		[DataType(DataType.Currency)]
		public double PricePerShare { get; set; }

		public CurrencyType Currency { get; set; }

		[Required(ErrorMessage = "Please enter the exchange rate.")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter the number greater or equal to 0.")]
		public double ExchangeRate { get; set; }

		[Required(ErrorMessage = "Please enter the total in eur.")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter the number greater or equal to 0.")]
		[DataType(DataType.Currency)]
		public double TotalInEur { get; set; }

		[Required(ErrorMessage = "Please enter the fees in eur.")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter the number greater or equal to 0.")]
		[DataType(DataType.Currency)]
		public double FeesInEur { get; set; }

		public string Username { get; set; } = string.Empty;
	}

	public static class TransactionViewModelMapper
	{
		public static Transaction ToDatabaseModel(this TransactionViewModel transactionViewModel) => new()
		{
			Id = transactionViewModel.Id ?? Guid.NewGuid().ToString(),
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

		public static TransactionViewModel FromDatabaseModel(this Transaction transaction) => new()
		{
			Id = transaction.Id,
			ActionType = transaction.Action,
			Time = transaction.Time,
			Ticker = transaction.Ticker,
			Name = transaction.Name,
			NumberOfShares = transaction.NumberOfShares,
			PricePerShare = transaction.PricePerShare,
			Currency = transaction.Currency,
			ExchangeRate = transaction.ExchangeRate,
			TotalInEur = transaction.TotalInEur,
			FeesInEur = transaction.FeesInEur,
			Username = transaction.Username
		};
	}
}
