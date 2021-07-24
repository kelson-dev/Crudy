using System;

namespace Crudy.Common
{
    public abstract class MatchingQuery<T, TId>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public abstract Func<T> Matching();

        public abstract string ToSql();
    }

    public abstract class PredicateQuery<T, TId>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public abstract Func<T, bool> Matching();

        public abstract string WhereClause();
    }

    public abstract class ParameterizedPredicateQuery<T, TId, TParam>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public abstract Func<T, TParam, bool> Matching();

        public abstract string WhereClause(TParam param);
    }
}
