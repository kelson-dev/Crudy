using Crudy.Common.Sql.TypeResolvers;
using System;
using System.Data;

namespace Crudy.Common.Sql.TypeResolvers
{
    public class BooleanHandler : TypeHandler<bool, bool>
    {
        public override DbType GetDbType(Column column) => DbType.Boolean;

        public override string GetDbTypeString(Column column) => "BIT";

        public override bool Deserialize(bool value) => value;

        public override bool Serialize(bool value) => value;
    }

    public class ByteArrayHandler : TypeHandler<byte[], byte[]>
    {
        public override DbType GetDbType(Column column) => DbType.Binary;

        public override string GetDbTypeString(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out FixedWidthAttribute f))
            //    return $"BINARY({f.Width})";
            //else if (column.TryGetAttribute(out MaxWidthAttribute m))
            //    return $"VARBINARY({m.Width})";
            //else
            //    return $"VARBINARY(MAX)";
        }

        public override byte[] Deserialize(byte[] value) => value;

        public override byte[] Serialize(byte[] value) => value;
    }

    public class DateTimeHandler : TypeHandler<DateTime, DateTime>
    {
        public override DbType GetDbType(Column column) => DbType.DateTime;

        public override string GetDbTypeString(Column column) => "DATETIME";

        public override DateTime Deserialize(DateTime value) => value;

        public override DateTime Serialize(DateTime value) => value;
    }

    public class DateTimeOffsetHandler : TypeHandler<DateTimeOffset, DateTimeOffset>
    {
        public override DbType GetDbType(Column column) => DbType.DateTimeOffset;

        public override string GetDbTypeString(Column column) => "DATETIMEOFFSET";

        public override DateTimeOffset Deserialize(DateTimeOffset value) => value;

        public override DateTimeOffset Serialize(DateTimeOffset value) => value;
    }

    public class DecimalHandler : TypeHandler<decimal, decimal>
    {
        public override DbType GetDbType(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out CurrencyAttribute currency))
            //    return DbType.Currency;
            //else
            //    return DbType.Decimal;
        }

        public override string GetDbTypeString(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out DecimalPrecisionAttribute precision))
            //    return $"DECIMAL({precision.Total}, {precision.Fractional})";
            //else if (column.TryGetAttribute(out CurrencyAttribute currency))
            //    return $"DECIMAL({currency.Total}, {currency.Fractional})";
            //else
            //    return $"DECIMAL(64, 32)";
        }

        public override decimal Deserialize(decimal value) => value;

        public override decimal Serialize(decimal value) => value;
    }

    public class DoubleHandler : TypeHandler<double, double>
    {
        public override DbType GetDbType(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out CurrencyAttribute currency))
            //    return DbType.Currency;
            //else
            //    return DbType.Decimal;
        }

        public override string GetDbTypeString(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out DecimalPrecisionAttribute precision))
            //    return $"DECIMAL({precision.Total}, {precision.Fractional})";
            //else if (column.TryGetAttribute(out CurrencyAttribute currency))
            //    return $"DECIMAL({currency.Total}, {currency.Fractional})";
            //else
            //    return "REAL";
        }

        public override double Deserialize(double value) => value;

        public override double Serialize(double value) => value;
    }

    public class FloatHandler : TypeHandler<float, float>
    {
        public override DbType GetDbType(Column column) => DbType.Single;

        public override string GetDbTypeString(Column column) => "REAL";

        public override float Deserialize(float value) => value;

        public override float Serialize(float value) => value;
    }

    public class GuidHandler : TypeHandler<Guid, byte[]>
    {
        public override DbType GetDbType(Column column) => DbType.Binary;

        public override string GetDbTypeString(Column column) => "BINARY(16)";

        public override Guid Deserialize(byte[] value) => new Guid(value);

        public override byte[] Serialize(Guid value) => value.ToByteArray();
    }

    public class Int16Handler : TypeHandler<short, short>
    {
        public override DbType GetDbType(Column column) => DbType.Int16;

        public override string GetDbTypeString(Column column) => "SMALLINT";

        public override short Deserialize(short value) => value;

        public override short Serialize(short value) => value;
    }

    public class Int32Handler : TypeHandler<int, int>
    {
        public override DbType GetDbType(Column column) => DbType.Int32;

        public override string GetDbTypeString(Column column) => "INT";

        public override int Deserialize(int value) => value;

        public override int Serialize(int value) => value;
    }

    public class Int64Handler : TypeHandler<long, long>
    {
        public override DbType GetDbType(Column column) => DbType.Int64;

        public override string GetDbTypeString(Column column) => "BIGINT";

        public override long Deserialize(long value) => value;

        public override long Serialize(long value) => value;
    }

    public class Int8Handler : TypeHandler<sbyte, sbyte>
    {
        public override DbType GetDbType(Column column) => DbType.SByte;

        public override string GetDbTypeString(Column column) => "TINYINT";

        public override sbyte Deserialize(sbyte value) => value;

        public override sbyte Serialize(sbyte value) => value;
    }

    public class StringHandler : TypeHandler<string, string>
    {
        public override DbType GetDbType(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out FixedWidthAttribute f))
            //    return DbType.StringFixedLength;
            //else
            //    return DbType.String;
        }

        public override string GetDbTypeString(Column column)
        {
            throw new NotImplementedException();
            //if (column.TryGetAttribute(out FixedWidthAttribute f))
            //    return $"NCHAR({f.Width})";
            //else if (column.TryGetAttribute(out MaxWidthAttribute m))
            //    return $"NVARCHAR({m.Width})";
            //else
            //    return $"NVARCHAR(MAX)";
        }

        public override string Deserialize(string value) => value;

        public override string Serialize(string value) => value;
    }

    public class UInt16Handler : TypeHandler<ushort, ushort>
    {
        public override DbType GetDbType(Column column) => DbType.UInt16;

        public override string GetDbTypeString(Column column) => "SMALLINT";

        public override ushort Deserialize(ushort value) => value;

        public override ushort Serialize(ushort value) => value;
    }

    public class UInt32Handler : TypeHandler<uint, uint>
    {
        public override DbType GetDbType(Column column) => DbType.UInt32;

        public override string GetDbTypeString(Column column) => "INT";

        public override uint Deserialize(uint value) => value;

        public override uint Serialize(uint value) => value;
    }

    public class UInt64Handler : TypeHandler<ulong, ulong>
    {
        public override DbType GetDbType(Column column) => DbType.UInt64;

        public override string GetDbTypeString(Column column) => "BIGINT";

        public override ulong Deserialize(ulong value) => value;

        public override ulong Serialize(ulong value) => value;
    }

    public class UInt8Handler : TypeHandler<byte, byte>
    {
        public override DbType GetDbType(Column column) => DbType.Byte;

        public override string GetDbTypeString(Column column) => "TINYINT";

        public override byte Deserialize(byte value) => value;

        public override byte Serialize(byte value) => value;
    }
}
