using System;

namespace Crudy.Common
{

    /// <summary>
    /// Indicates that a column that would otherwise be set by the DB should be explicitly provided on creation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class ExplicitAttribute : Attribute { }

    /// <summary>
    /// Indicates this value should be initially set by the storage layer
    /// It will reflect the time that the entity is intially created.
    /// On `long` fields: will reflect the UTC unix epoch
    /// On `string` fields: will reflect the ISO-8601 date time with offset to UTC
    /// On `DateTime` fields: will reflect the UTC date and time
    /// On `DateTimeOffset` fields: will reflect the local system date and time
    /// Not compatible with other types, including SqlDateTime
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class CreatedAttribute : Attribute { }

    /// <summary>
    /// Indicates this value should be set by the storage layer on every write or update.
    /// It will reflect the time that the entity is intially was last modified.
    /// On `long` fields: will reflect the UTC unix epoch
    /// On `string` fields: will reflect the ISO-8601 date time with offset to UTC
    /// On `DateTime` fields: will reflect the UTC date and time
    /// On `DateTimeOffset` fields: will reflect the local system date and time
    /// Not compatible with other types, including SqlDateTime
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class UpdatedAttribute : Attribute { }

    /// <summary>
    /// Will generate a random value on initial creation of the entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class CreationRandomAttribute : Attribute { }

    /// <summary>
    /// Will generate a random value on each write or update of the entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class UpdateRandomAttribute : Attribute { }

    /// <summary>
    /// Will increment by one unit on each write or update of the entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class IncrementAttribute : Attribute { }

    /// <summary>
    /// Indicates that the field should be indexed or organized searchabely in the storage layer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class IndexAttribute : Attribute { }

    /// <summary>
    /// Indicates that the field should have a fixed size.
    /// Use on strings or arrays of primitives.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class FixedWidthAttribute : Attribute
    {
        public uint Width { get; }
        public FixedWidthAttribute(uint width) => Width = width;
    }

    /// <summary>
    /// Indicates that a field will fit within the specified max width (inclusive).
    /// Use on strings or arrays of primitives.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class MaxWidthAttribute : Attribute
    {
        public uint Width { get; }
        public MaxWidthAttribute(uint width) => Width = width;
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
