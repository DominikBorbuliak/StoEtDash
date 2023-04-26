using System.Security.Cryptography;
using System.Text;

namespace StoEtDash.Web.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// Converts string to sha512 hash
		/// Code taken from: https://www.techiedelight.com/generate-sha-512-hash-for-input-data-csharp/
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ToSha512(this string input)
		{
			var data = Encoding.UTF8.GetBytes(input);
			var hashedData = SHA512.HashData(data);

			var stringBuilder = new StringBuilder();
			foreach (var b in hashedData)
			{
				stringBuilder.Append($"{b:x2}");
			}

			return stringBuilder.ToString();
		}
	}
}
