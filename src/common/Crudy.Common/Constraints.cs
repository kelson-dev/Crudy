using System;
using System.Collections;

namespace Crudy.Common
{
    public class ConstraintValidationException : Exception { }

    public interface Constraint<T> : ColumnDescriptor<T>
    {
        T Value { get; init; }
    }

    public abstract class Size
    {
        public abstract uint Value { get; }
    }

    public abstract class Precision
    {
        public abstract uint Whole { get; }
        public abstract uint Fractional { get; }
    }

    public class Currency : Precision
    {
        public override uint Whole => 16;
        public override uint Fractional => 2;
    }

    /// <summary>
    /// Indicates that a field will fit within the specified max width (inclusive).
    /// Use on strings or arrays of primitives.
    /// </summary>
    public readonly struct MaxWidth<T, TSize> : Constraint<T>
        where TSize : Size, new()
    {
        private static readonly TSize size = new();

        private readonly T _value;
        public T Value
        {
            get => _value;
            init => _value = Validate(value);
        }
        
        public static T Validate(T value)
        {
            if (value is string text && text.Length > size.Value
                || value is Array array && array.Length > size.Value
                || value is ICollection collection && collection.Count > size.Value)
                throw new ConstraintValidationException();
            return value;
        }

        public static implicit operator T(MaxWidth<T, TSize> value) => value.Value;
        public static implicit operator MaxWidth<T, TSize>(T value) => new() { Value = value };
    }

    public readonly struct DecimalPrecision<T, TPrecision>
        where TPrecision : Precision
    {
        public T Value { get; init; }

        public static implicit operator T(DecimalPrecision<T, TPrecision> value) => value.Value;
        public static implicit operator DecimalPrecision<T, TPrecision>(T value) => new() { Value = value };
    }
}
