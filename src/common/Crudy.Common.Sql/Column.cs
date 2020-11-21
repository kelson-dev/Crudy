using System;

namespace Crudy.Common.Sql
{
    public record Column(Type Type, string ColumnName, string PropertyName, Attribute[] Attributes)
    {
        public bool TryGetAttribute<T>(out T attribute) where T : Attribute
        {
            foreach (var current in Attributes)
            {
                if (current is T found)
                {
                    attribute = found;
                    return true;
                }
            }
            attribute = default;
            return false;
        }
    };
}
