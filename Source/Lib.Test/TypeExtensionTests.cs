using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeExtensions;

namespace Lib.Test
{
	[TestClass]
	public class TypeExtensionTests
	{
		#region Implements
		[TestMethod, TestCategory("Unit")]
		public void Implements_Throws_ArgumentNullException()
		{
			Type type = null;
			Action act = () => type.Implements<IEnumerable>();

			act.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod, TestCategory("Unit")]
		public void Implements_ReturnsTrue_ForTheTypeItself()
		{
			Type type = typeof(SomeClass);
			type.Implements<SomeClass>().Should().BeTrue("because it is the same type");
		}

		[TestMethod, TestCategory("Unit")]
		public void Implements_CorrectlyIdentifies_IfTypeImplementsInterface()
		{
			Type type = typeof(SomeClass);

			type.Implements<ISomeInterface>().Should().BeTrue("because SomeClass implements interface ISomeInterface");
		}

		[TestMethod, TestCategory("Unit")]
		public void Implements_CorrectlyDetermines_IfTypeDoesNotImplementInterface()
		{
			Type type = typeof(SomeClass);
			type.Implements<IUnused>().Should().BeFalse("because SomeClass does not implement IUnused");
		}

		[TestMethod, TestCategory("Unit")]
		public void Implements_CorrectlyDetermines_IfTypeImplementsBaseType()
		{
			Type type = typeof(DerivedClass);
			type.Implements<SomeClass>().Should().BeTrue("because DerivedClass implements SomeClass");
		}
		#endregion

		#region ImplementsT
		[TestMethod, TestCategory("Unit")]
		public void ImplementsT_ThrowsArgumentNullException_IfTypeIsNull()
		{
			Type type = null;
			Action act = () => type.ImplementsInterface(typeof(ISomeInterface));

			act.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod, TestCategory("Unit")]
		public void ImplementsT_ThrowsArgumentNullException_IfInterfaceIsNull()
		{
			Type type = typeof(IEnumerable);
			Type interfaceType = null;
			Action act = () => type.ImplementsInterface(interfaceType);

			act.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod, TestCategory("Unit")]
		public void ImplementsT_ThrowsArgumentException_IfTargetIsNotInterface()
		{
			Type type = typeof(DerivedClass);
			Type interfaceType = typeof(SomeClass);
			Action act = () => type.ImplementsInterface(interfaceType);

			act.ShouldThrow<ArgumentException>();
		}

		[TestMethod, TestCategory("Unit")]
		public void ImplementsT_CanDetermine_IfInterfaceIsImplemented()
		{
			Type type = typeof(DerivedClass);
			Type interfaceType = typeof(ISomeInterface);

			type.ImplementsInterface(interfaceType).Should().BeTrue("because the interface is implemented by base class");
		}
		#endregion

		#region ImplementsInterface{T}
		[TestMethod, TestCategory("Unit")]
		public void ImplementsInterfaceT_CanDetermine_IfInterfaceIsImplementedDirectly()
		{
			Type type = typeof(ClassType);
			Type interfaceType = typeof(IInterfaceType);

			type.ImplementsInterface(interfaceType).Should().BeTrue("because the interface is implemented directly by class");
		}

		[TestMethod, TestCategory("Unit")]
		public void ImplementsInterfaceT_CanDetermine_IfInterfaceIsImplementedThroughGenericType()
		{
			var theType = new GenericClass<int>();
			Type sourceType = theType.GetType();
			Type interfaceType = typeof(IInterfaceType);

			sourceType.ImplementsInterface(interfaceType).Should().BeTrue("because the interface is implemented directly by class");
		}
		#endregion

		#region GetCustomAttributesIncludingInterfaceAttributes
		[TestMethod, TestCategory("Unit")]
		public void GetCustomAttributesIncludingInterfaceAttributes_Throws_ArgumentNullException()
		{
			Type theType = null;
			Action act = () => theType.GetCustomAttributesIncludingInterfaceAttributes<AnotherAttribute>();

			act.ShouldThrow<ArgumentNullException>("because the method cannot be invoked on null type");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetCustomAttributesIncludingInterfaceAttributes_Returns_AttributesAppliedToType()
		{
			Type theType = typeof(TypeWithAttribute);
			theType.GetCustomAttributesIncludingInterfaceAttributes<Attribute>()
				.Should()
				.NotBeNull()
				.And
				.HaveCount(2, "because there are two attributes applied");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetCustomAttributesIncludingInterfaceAttributes_Returns_AttributesInheritedFromInterface()
		{
			Type theType = typeof(DummyClass);
			theType.GetCustomAttributesIncludingInterfaceAttributes<AnotherAttribute>()
				.Should()
				.NotBeNull()
				.And
				.HaveCount(1, "because there is only one attribute applied");
		}
		#endregion

		#region GetTypeConainedInArray
		[TestMethod, TestCategory("Unit")]
		public void GetTypeConainedInArray_ReturnsNull_IfTypeIsNull()
		{
			Type type = null;

			type.GetTypeConainedInArray().Should().BeNull("because the type is null");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetTypeConainedInArray_ReturnsInnerType()
		{
			string[] array = { null };
			array.GetType().GetTypeConainedInArray().Should().Be<string>("because the array is of type System.string");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetTypeConainedInArray_ReturnsType_IfTypeIsNotArray()
		{
			string myString = string.Empty;
			myString.GetType().GetTypeConainedInArray().Should().Be<string>("because the array is of type System.string");
		}
		#endregion

		#region GetInnerTypeOfGenericOrArray

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericOrArray_ReturnsNull_IfTypeIsNull()
		{
			Type type = null;

			type.GetInnerTypeOfGenericOrArray().Should().BeNull("because the type is null");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericOrArray_ReturnsTypeofParameter()
		{
			Type type = typeof(GenericClass<int>);

			type.GetInnerTypeOfGenericOrArray().Should().Be<int>("because the type param is System.Int32");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericOrArray_ReturnsTypeofParameter_ForArrayType()
		{
			Type type = typeof(string[]);

			type.GetInnerTypeOfGenericOrArray().Should().Be<string>("because the type param is System.String");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericOrArray_ReturnsType_IfTypeIsNotArrayOrGeneric()
		{
			Type type = typeof(SomeClass);

			type.GetInnerTypeOfGenericOrArray().Should().Be<SomeClass>("because the type is not an array or generic");
		}

		#endregion

		#region IsGenericListOrEnumerable

		[TestMethod, TestCategory("Unit")]
		public void IsGenericListOrEnumerable_ReturnsFalse_IfTypeIsNull()
		{
			Type type = null;

			type.IsGenericListOrEnumerable().Should().BeFalse("because the type is null");
		}

		[TestMethod, TestCategory("Unit")]
		public void IsGenericListOrEnumerable_ReturnsTrue_IfTypeIsGenericList()
		{
			Type type = typeof(List<>);

			type.IsGenericListOrEnumerable().Should().BeTrue("because the type is a generic list");
		}

		[TestMethod, TestCategory("Unit")]
		public void IsGenericListOrEnumerable_ReturnsFalse_IfTypeIsIEnumerable()
		{
			Type type = typeof(IEnumerable<>);

			type.IsGenericListOrEnumerable().Should().BeTrue("because the type is IEnumerable");
		}

		[TestMethod, TestCategory("Unit")]
		public void IsGenericListOrEnumerable_ReturnsFalse_IfTypeIsString()
		{
			Type type = typeof(string);

			type.IsGenericListOrEnumerable().Should().BeFalse("because the type is not a list or enumerable");
		}

		[TestMethod, TestCategory("Unit")]
		public void IsGenericListOrEnumerable_ReturnsFalse_IfTypeIsArray()
		{
			Type type = typeof(int[]);

			type.IsGenericListOrEnumerable().Should().BeFalse("because the type is an array");
		}
		#endregion

		#region GetInnerTypeOfGenericCollectionOrArray

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericCollectionOrArray_ReturnsNull_IfTypeIsNull()
		{
			Type type = null;

			type.GetInnerTypeOfGenericCollectionOrArray().Should().BeNull("because the type is null");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericCollectionOrArray_ReturnsInnerType_IfTypeIsGenericList()
		{
			IList<int> myList = new List<int>();
			Type type = myList.GetType();

			type.GetInnerTypeOfGenericCollectionOrArray().Should().Be<int>("because we have passed a List of int");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericCollectionOrArray_ReturnsInnerType_IfTypeIsICollection()
		{
			ICollection<string> myCollection = new ReadOnlyCollection<string>(new[] { "one", "two" });
			Type type = myCollection.GetType();

			type.GetInnerTypeOfGenericCollectionOrArray().Should().Be<string>("because we have passed an ICollection of string");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericCollectionOrArray_ReturnsInnerType_IfTypeIsIEnumerable()
		{
			IEnumerable<decimal> myDecimals = new List<decimal>();
			Type type = myDecimals.GetType();

			type.GetInnerTypeOfGenericCollectionOrArray().Should().Be<decimal>("because we have passed an IEnumerable of decimal");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericCollectionOrArray_ReturnsInnerType_IfTypeIsArray()
		{
			decimal[] myDecimals = new[] { 1.9m, 2.0m };
			Type type = myDecimals.GetType();

			type.GetInnerTypeOfGenericCollectionOrArray().Should().Be<decimal>("because we have passed an IEnumerable of decimal");
		}

		[TestMethod, TestCategory("Unit")]
		public void GetInnerTypeOfGenericCollectionOrArray_ReturnsInnerType_IfTypeIsNullable()
		{
			Nullable<decimal> myDecimal = 1.0m;
			Type type = myDecimal.GetType();

			type.GetInnerTypeOfGenericCollectionOrArray().Should().Be<decimal>("because we have passed a Nullable decimal");
		}
		#endregion
	}
}
