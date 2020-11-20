using System;

namespace Crudy.Common
{
    public abstract class MatchingQuery<T, TId>
        where T : IExistingEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public abstract Func<T> Matching();
    }

    public abstract class PredicateQuery<T, TId>
        where T : IExistingEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public abstract Func<T, bool> Matching();
    }

    public abstract class ParameterizedPredicateQuery<T, TId, TParam>
        where T : IExistingEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public abstract Func<T, TParam, bool> Matching();

        protected ParameterizedPredicateQuery(TParam param) { }
    }
}
