using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TypeExtensions
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Determines if a type implements the specified target type.
		/// </summary>
		/// <typeparam name="TTarget">The base type that should be implementedof the target.</typeparam>
		/// <param name="theType">The Type to check.</param>
		/// <returns>True if sourceType implements the interface, false otherwise</returns>
		/// <exception cref="System.ArgumentNullException">targetType</exception>
		public static bool Implements<TTarget>(this Type theType) where TTarget : class
		{
			if (theType == null) throw new ArgumentNullException("theType");

			var baseType = typeof(TTarget);
			return theType == baseType || baseType.IsAssignableFrom(theType);
		}

		/// <summary>
		/// Determines if the source type implements the  target interface.
		/// </summary>
		/// <param name="sourceType">The type.</param>
		/// <param name="targetInterfaceType">Type of the target interface.</param>
		/// <returns>True if sourceType implements the targetinterface, false otherwise</returns>
		/// <exception cref="System.ArgumentNullException">
		/// type
		/// or
		/// targetInterfaceType
		/// </exception>
		/// <exception cref="System.ArgumentException">Expect an interface;targetInterfaceType</exception>
		public static bool ImplementsInterface(this Type sourceType, Type targetInterfaceType)
		{
			if (sourceType == null) throw new ArgumentNullException("sourceType");
			if (targetInterfaceType == null) throw new ArgumentNullException("targetInterfaceType");
			if (!targetInterfaceType.IsInterface) throw new ArgumentException("An interface was expected", "targetInterfaceType");

			Func<Type, bool> implementsInterface = targetInterfaceType.IsAssignableFrom;
			if (targetInterfaceType.IsGenericType)
			{
				implementsInterface = t => t.IsGenericType && targetInterfaceType.IsAssignableFrom(t.GetGenericTypeDefinition());
			}

			return implementsInterface(sourceType) || sourceType.GetInterfaces().Any(implementsInterface);
		}

		/// <summary>
		/// Determines if a type implements the interface through the generic type parameter.
		/// </summary>
		/// <typeparam name="TInterface">The type of the interface.</typeparam>
		/// <param name="type">The type that has to be checked.</param>
		/// <returns>True if the type has TInterface as type param</returns>
		public static bool ImplementsInterface<TInterface>(this Type type) where TInterface : class
		{
			return ImplementsInterface(type, typeof(TInterface));
		}

		/// <summary>
		/// Gets the custom attributes including those applied on the inherited interface.
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute.</typeparam>
		/// <param name="type">The type to check.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="TAttribute"/></returns>
		/// <exception cref="System.ArgumentNullException">type</exception>
		public static IEnumerable<TAttribute> GetCustomAttributesIncludingInterfaceAttributes<TAttribute>(this Type type) 
			where TAttribute : Attribute
		{
			if (type == null) throw new ArgumentNullException("type");

			var attributeType = typeof(TAttribute);
			var result =
				type.GetCustomAttributes(attributeType, true)
					.Union(
						type.GetInterfaces()
							.SelectMany(
								interfaceType => interfaceType.GetCustomAttributes(attributeType, true)))
					.Distinct()
					.Cast<TAttribute>();

			return result;
		}

		/// <summary>
		/// Gets the type contained in array.
		/// </summary>
		/// <param name="type">The array type.</param>
		/// <returns>
		/// The type encompassed by the array.
		/// </returns>
		public static Type GetTypeConainedInArray(this Type type)
		{
			if (type == null) return null;

			if (type.IsArray && type.HasElementType)
			{
				return type.GetElementType();
			}

			return type;
		}

		/// <summary>
		/// Gets the inner type of a generic or array type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The type of the parameter in a generic type or the type of elements in an array</returns>
		public static Type GetInnerTypeOfGenericOrArray(this Type type)
		{
			if (type == null) return null;

			if (type.IsGenericType && type.GetGenericArguments().Any())
			{
				var innerType = type.GetGenericArguments().FirstOrDefault();
				return innerType ?? type.GetTypeConainedInArray();
			}

			return type.GetTypeConainedInArray();
		}

		/// <summary>
		/// Gets the inner type of generic collection or array.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The type encompassed by the generic collection or array</returns>
		public static Type GetInnerTypeOfGenericCollectionOrArray(this Type type)
		{
			if (type == null) return null;

			return type.IsGenericListOrEnumerable() ? type.GetInnerTypeOfGenericOrArray() : type.GetInnerTypeOfGenericOrArray();
		}

		/// <summary>
		/// Determines whether the given type is derived from IEnumerable.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static bool IsGenericListOrEnumerable(this Type type)
		{
			var result = type != null && type.IsGenericType && type.ImplementsInterface<IEnumerable>();
			return result;
		}
	}
}
