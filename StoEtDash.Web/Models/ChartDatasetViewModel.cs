using Newtonsoft.Json;

namespace StoEtDash.Web.Models
{
	public class ChartDatasetViewModel
	{
		[JsonProperty("data")]
		public List<double> Data { get; set; }

		[JsonProperty("backgroundColor")]
		public List<string> BackgroundColors => Enumerable.Repeat(Colors, (int)Math.Ceiling((double)Data.Count / Colors.Count))
			.SelectMany(color => color)
			.Take(Data.Count)
			.ToList();

		private readonly List<string> Colors = new() { "#DDD9FF", "#5D1444", "#BAB2FE", "#8C1E66", "#7465FD", "#BA2887", "#8656E0", "#A938A5", "#9747C2" };
	}
}
