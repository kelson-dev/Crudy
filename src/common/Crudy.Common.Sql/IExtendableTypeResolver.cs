using System;
using System.Data;

namespace Crudy.Common.Sql
{
    public interface IExtendableTypeResolver
    {
        DbType GetDbType(Column column);
        string GetDbTypeString(Column column);
        object DbSerialize(Type type, object value);
        object? DbDeserialize(Type type, object value);
        bool IsOneueType(Type type);
        bool IsReference(Type type);
    }
}
