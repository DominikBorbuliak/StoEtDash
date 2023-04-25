using Newtonsoft.Json;

namespace StoEtDash.Web.Models
{
	public class ChartDatasetViewModel
	{
		[JsonProperty("label")]
		public string? Label { get; set; }

		[JsonProperty("data")]
		public List<double>? Data { get; set; }

		[JsonProperty("backgroundColor")]
		public List<string>? BackgroundColors { get; set; }

		[JsonProperty("borderColor")]
		public string BorderColor { get; set; } = "#FFFFFF";

		[JsonProperty("hoverOffset")]
		public int HowerOffset = 10;
	}
}
