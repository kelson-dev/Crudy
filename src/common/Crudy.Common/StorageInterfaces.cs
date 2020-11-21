using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crudy.Common
{
    /// <summary>
    /// To be implemented by source generators as implicitly castable to IReadWriteEntityStorage implementations for each entity type
    /// </summary>
    public interface IStorage
    {

    }

    /// <summary>
    /// To be implemented by source generators as implicitly castable to IReadableEntityStorage implementations for each entity type
    /// </summary>
    public interface IReadOnlyStorage
    {

    }

    public interface IReadWriteEntityStorage<T, TId> :
        IReadableEntityStorage<T, TId>,
        IWritableEntityStorage<T, TId>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {

    }

    public interface IReadableEntityStorage<T, TId> 
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        Task<SortedList<TId, T>> Read();
        Task<T> Read(TId id);
        Task<SortedList<TId, T>> Read(MatchingQuery<T, TId> matching);
        Task<SortedList<TId, T>> Read(PredicateQuery<T, TId> predicate);
        Task<SortedList<TId, T>> Read<TParam>(ParameterizedPredicateQuery<T, TId, TParam> predicate, params TParam[] param);
    }

    public interface IWritableEntityStorage<T, TId>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        Task<T> Write(INewEntity<T, TId> value);
        Task<SortedList<TId, T>> WriteMany(params INewEntity<T, TId>[] values);
        Task<SortedList<TId, T>> Update(params T[] values);
        Task<SortedList<TId, T>> Update(PredicateQuery<T, TId> predicate, MatchingQuery<T, TId> to);
        Task<int> Delete(params TId[] values);
        Task<int> Delete(MatchingQuery<T, TId> matching);
        Task<int> Delete(PredicateQuery<T, TId> predicate);
        Task<int> Delete<TParam>(ParameterizedPredicateQuery<T, TId, TParam> predicate, params TParam[] param);
    }
}
