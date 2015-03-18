using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeExtensions
{
	public static class MathExtensions
	{
		/// <summary> Rounds down a double floating point number without rounding off like Math.Round(decimal value, int digits). </summary>
		/// <param name="value">The value.</param>
		/// <param name="digitsToRoundTo">The digits to round to.</param>
		/// <returns>The int part of the number without rounding or truncation</returns>
		public static decimal RoundDown(this decimal value, int digitsToRoundTo)
		{
			int factor = (int)Math.Pow(10, digitsToRoundTo);
			return Math.Truncate(value * factor) / factor;
		}

		/// <summary> Rounds down a double floating point number without rounding off like Math.Round(double value, int digits). </summary>
		/// <param name="value">The value.</param>
		/// <param name="digitsToRoundTo">The number of digits after decimal to round to.</param>
		/// <returns>A <see cref="double"/> value, appropriately rounded down.</returns>
		public static double RoundDown(this double value, int digitsToRoundTo)
		{
			int factor = (int)Math.Pow(10, digitsToRoundTo);
			return Math.Truncate(value * factor) / factor;
		}

		/// <summary>
		/// Returns a sequence of integers between start and end (both included).
		/// </summary>
		/// <param name="start">The start of the sequence.</param>
		/// <param name="end">The end of the sequence.</param>
		public static IEnumerable<int> Range(this int start, int end)
		{
			return start > end
					   ? Enumerable.Range(end, start - end + 1).Reverse()
					   : Enumerable.Range(start, end - start + 1);
		}
	}
}
