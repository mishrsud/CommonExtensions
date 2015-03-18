using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeExtensions;

namespace Lib.Test
{
	[TestClass]
	public class MathExtensionTests
	{
		[TestMethod, TestCategory("Unit")]
		public void DecimalRoundDown_Correctly_Trims_Decimals()
		{
			decimal number = 23.56m;
			var result = number.RoundDown(1);

			result.Should().Be(23.5m);
		}

		[TestMethod, TestCategory("Unit")]
		public void DoubleRoundDown_Correctly_Trims_Decimals()
		{
			double number = 123.567;
			var result = number.RoundDown(2);

			result.Should().Be(123.56);
		}

		[TestMethod, TestCategory("Unit")]
		public void Range_ReturnsSequenceOfCount1_IfStartIsSameAsEnd()
		{
			IEnumerable<int> sequence = 1.Range(1);

			sequence.Should().HaveCount(1);
		}

		[TestMethod, TestCategory("Unit")]
		public void Range_ReturnsSequenceOfExpectedCount()
		{
			IEnumerable<int> sequence = 1.Range(5);

			sequence.Should().HaveCount(5);
		}
	}
}
