using System;
using System.Data;

namespace Crudy.Common.Sql
{

    public abstract class GenericSqlDao<TDbConnection, TDbCommand, TDbReader>
        where TDbConnection : IDbConnection 
        where TDbCommand : IDbCommand 
        where TDbReader : IDataReader
    {
        protected readonly IDataConsumer<TDbReader> consumer;
        protected readonly IExtendableTypeResolver types;
        protected readonly Func<IStorage> db;
    }
}
