using Newtonsoft.Json;

namespace StoEtDash.Web.Models
{
	public class ChartDataViewModel
	{

		[JsonProperty("labels")]
		public List<string>? Labels { get; set; }

		[JsonProperty("datasets")]
		public List<ChartDatasetViewModel>? Datasets { get; set; }
	}
}
