using System;

namespace Crudy.Common.Sql
{
    public record Column(Type Type, string PropertyName)
    {
        private string? _columnName = null;
        public string ColumnName => _columnName ?? PropertyName;

        public string ToColumnSql() => throw new NotImplementedException();
    };
}
