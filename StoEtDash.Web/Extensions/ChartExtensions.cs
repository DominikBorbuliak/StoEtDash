namespace StoEtDash.Web.Extensions
{
	public static class ChartExtensions
	{
		private static readonly List<string> _colors = new() { "#DDD9FF", "#5D1444", "#BAB2FE", "#8C1E66", "#7465FD", "#BA2887", "#8656E0", "#A938A5", "#9747C2" };

		public static List<string> GetNColors(int n) => Enumerable.Repeat(_colors, (int)Math.Ceiling((double)n / _colors.Count))
				.SelectMany(color => color)
				.Take(n)
				.ToList();
	}
}
