using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace Crudy.Common.Gen
{
    public static class Extensions
    {
        public static IEnumerable<V> WhereIs<U, V>(this IEnumerable<U> list)
            where V : U
        {
            foreach (var item in list)
                if (item is V expected)
                    yield return expected;
        }

        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<TValue> values, Func<TValue, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            var list = values is ICollection<TValue> collection
                ? new SortedList<TKey, TValue>(collection.Count)
                : new SortedList<TKey, TValue>();
            foreach (var value in values)
                list.Add(keySelector(value), value);
            return list;
        }
    }
}
