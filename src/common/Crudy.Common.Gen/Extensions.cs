using Microsoft.CodeAnalysis;
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
    }
}
