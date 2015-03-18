using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeExtensions;

namespace Lib.Test
{
	[TestClass]
	public class EnumerableExtensionTests
	{
		[TestMethod, TestCategory("Unit")]
		public void UniqueBy_ReturnsUniqueElements_ForListOfPrimitive()
		{
			var collection = new List<int>() { 1, 2, 3, 4, 4, 5, 6, 6, 6 };
			var filteredCollection = collection.UniqueBy(i => i);

			filteredCollection
				.Should()
				.NotBeNull()
				.And
				.HaveCount(6, "because there are 6 unique elements");
		}

		[TestMethod, TestCategory("Unit")]
		public void UniqueBy_ReturnsUniqueElements_ForListOfObjects()
		{
			var collection = new List<TestAccount>()
			{
				new TestAccount() {AccountId = "acc1", Balance = 2000m},
				new TestAccount() {AccountId = "acc1", Balance = 2000m},
			};

			var filteredCollection = collection.UniqueBy(obj => obj.AccountId);

			filteredCollection
				.Should()
				.NotBeNull()
				.And
				.HaveCount(1, "because there is 1 unique object");
		}

		[TestMethod, TestCategory("Unit")]
		public void ListToDatatable_ConvertsListToDatatable()
		{
			var collection = new List<TestAccount>()
			{
				new TestAccount() {AccountId = "acc1", Balance = 2000m},
				new TestAccount() {AccountId = "acc2", Balance = 5000m},
			};

			var dataTable = collection.ToDataTable();

			Assert.IsTrue(dataTable.Rows.Count == 2);
		}
	}
}
