using System;
using System.Collections;

namespace Crudy.Common
{
    public class ConstraintValidationException : Exception
    {

    }

    public interface Constraint<T>
    {
        T Value { get; init; }
    }

    public abstract class Size
    {
        public abstract uint Value { get; }
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

    /// <summary>
    /// Indicates that the field is used to store currency values and 
    /// should use decimal precision values accordingly
    /// </summary>
    public readonly struct Currency<T>
    {
        public T Value { get; init; }
        
        public static implicit operator T(Currency<T> value) => value.Value;
        public static implicit operator Currency<T>(T value) => new() { Value = value };
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class DecimalPrecisionAttribute : Attribute
    {
        public readonly int Total;
        public readonly int Fractional;

        public DecimalPrecisionAttribute(int total, int fractional)
        {
            Total = total;
            Fractional = fractional;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class CurrencyAttribute : DecimalPrecisionAttribute
    {
        public CurrencyAttribute() : base(16, 2) { }
    }
}
