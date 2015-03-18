using System;
using NUnit.Framework.Constraints;

namespace Lib.Test
{
	/*
	 * This File holds objects required for tests, do not move!
	 */

	[AttributeUsage(AttributeTargets.All)]
	internal class MyTestAttribute : Attribute
	{
		public string Name { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	internal class AnotherAttribute : Attribute
	{
		public int Id { get; set; }
	}

	[MyTest, Another]
	internal class TypeWithAttribute
	{
	}

	[Another]
	internal interface IDummyInterface
	{
		
	}

	internal class DummyClass : IDummyInterface
	{
		
	}

	#region Test Interfaces and Implementations
	internal interface ISomeInterface
	{
		// Intentionally empty
	}

	internal class SomeClass : ISomeInterface
	{
		// Intentionally empty
	}

	internal class DerivedClass : SomeClass
	{
		// Intentionally Empty 
	}

	internal interface IUnused
	{
		// Intentionally empty
	}

	internal interface IInterfaceType
	{
		string Property { get; }
	}

	internal class ClassType : IInterfaceType
	{
		public string Property
		{
			get { return "Test"; }
		}
	}

	internal class GenericClass<T> : IInterfaceType
	{
		public string Property
		{
			get { return "Test"; }
		}
	}

	internal class TestAccount
	{
		public string AccountId { get; set; }
		public decimal Balance { get; set; }
	}

	#endregion
}
