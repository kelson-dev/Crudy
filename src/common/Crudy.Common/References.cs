using System;
using System.Threading.Tasks;

namespace Crudy.Common
{
    public partial struct One<T, TId>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public TId ID { get; set; }
        private T? _item;
        public async Task<T> Fetch(IReadableEntityStorage<T, TId> storage) => _item ??= await storage.Read(ID);
        public static implicit operator TId(One<T, TId> one) => one.ID;
        public static implicit operator One<T, TId>(TId id) => new() { ID = id };
    }

    public partial struct Optional<T, TId>
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public TId? ID { get; set; }
        private T? _item;
        public async Task<T?> Fetch(IReadableEntityStorage<T, TId> storage) => ID is TId present 
            ? _item ??= await storage.Read(present)
            : default;
        public static implicit operator TId(Optional<T, TId> one) => one.ID;
        public static implicit operator Optional<T, TId>(TId id) => new() { ID = id };
    }
}
