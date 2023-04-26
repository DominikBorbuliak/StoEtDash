using StoEtDash.Web.Database.Models;
using System.Reflection;

namespace StoEtDash.Web.Extensions
{
	public static class EnumExtensions
	{
		/// <summary>
		/// Get currency display format of enum value
		/// </summary>
		/// <typeparam name="E">Type of enum</typeparam>
		/// <param name="value">Enum value</param>
		/// <returns>
		/// Currency display format of enum value - if exists
		/// string.Empty - if currency display format is missing or empty</returns>
		public static string GetCurrencyDisplayFormat<E>(this E value) where E : Enum => value.GetAttribute<E, CurrencyDisplayFormatAttribute>()?.CurrencyDisplayFormat ?? string.Empty;

		/// <summary>
		/// Get time series function name of enum value
		/// </summary>
		/// <typeparam name="E">Type of enum</typeparam>
		/// <param name="value">Enum value</param>
		/// <returns>
		/// Time series function name - if exists
		/// string.Empty - if time series function name is missing or empty</returns>
		public static string GetTimeSeriesFunctioNname<E>(this E value) where E : Enum => value.GetAttribute<E, TimeSeriesFunctionNameAttribute>()?.TimeSeriesFunctionName ?? string.Empty;

		/// <summary>
		/// Get specified attribute of enum value
		/// </summary>
		/// <typeparam name="E">Enum type</typeparam>
		/// <typeparam name="A">Attribute type</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>
		/// Attribute - if exists
		/// null - if attribute is missing
		/// </returns>
		/// <exception cref="ArgumentException"></exception>
		private static A? GetAttribute<E, A>(this E enumValue)
			where E : Enum
			where A : Attribute
		{
			var fieldInfo = typeof(E).GetField(enumValue.ToString());

			// Value does not exists in current enum
			if (fieldInfo == null)
				throw new ArgumentException($"Enum value: '{enumValue}' does not exists in current enum type: '{typeof(E)}'");

			return fieldInfo.GetCustomAttribute<A>();
		}
	}
}
