using System.Collections.Generic;
using System.Data;

namespace Crudy.Common.Sql
{
    public interface IDataConsumer<TDataReader> where TDataReader : IDataReader
    {
        IEnumerable<string> Fields(TDataReader reader);
        object ReadColumn(TDataReader reader, Column column, int index, IExtendableTypeResolver types);
    }
}
