using Crudy.Common;
using Crudy.Common.Sql;
using System;
using System.Collections.Generic;

namespace Crudy.Mssql.Common
{


    internal class SqlServerTypeProvider : ExtendableTypeProvider
    {
        public SqlServerTypeProvider(Func<IStorage> db) : base(db) { }
    }
}
