using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace TypeExtensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Gets unique elements in the input sequence by the predicate passed.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="sourceSequence">The source sequence.</param>
		/// <param name="selectorPredicate">The selector predicate.</param>
		/// <returns></returns>
		public static IEnumerable<TSource> UniqueBy<TSource, TKey>(this IEnumerable<TSource> sourceSequence, Func<TSource, TKey> selectorPredicate)
		{
			HashSet<TKey> result = new HashSet<TKey>();

			foreach (TSource item in sourceSequence)
			{
				TKey key = selectorPredicate(item);
				if (result.Contains(key))
				{
					continue;
				}

				result.Add(key);
				yield return item;
			}
		}

		/// <summary> Converts the passed up <see cref="IList{T}"/> to a <see cref="DataTable"/>. </summary>
		/// <typeparam name="T">A type deriving from <see cref="IList{T}"/></typeparam>
		/// <param name="data">The data.</param>
		/// <returns>A <see cref="DataTable"/></returns>
		public static DataTable ToDataTable<T>(this IList<T> data)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
			var table = new DataTable();

			for (int i = 0; i < props.Count; i++)
			{
				PropertyDescriptor prop = props[i];
				table.Columns.Add(prop.Name, prop.PropertyType);
			}

			var values = new object[props.Count];

			foreach (T item in data)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = props[i].GetValue(item);
				}

				table.Rows.Add(values);
			}

			return table;
		}
	}
}
