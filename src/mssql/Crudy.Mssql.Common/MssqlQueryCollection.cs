using Crudy.Common;
using Crudy.Common.Sql;
using System;

namespace Crudy.Mssql.Common
{
    /// <summary>
    /// A single instance of this type will be created for each entity type in your assembly by the Crudy.Mssql.Gen package.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public record MssqlQueryCollection<T, TId>(
        Column[] Columns,
        Column[] NonIdColumns,
        Column IdColumn,
        string TableName,
        string CreateTableSql,
        string DropTableSql,
        string WhereIdPredicate,
        string DeletePrefix,
        string DeleteWhereId,
        string UpdatePrefix,
        string UpdateAllColumns,
        string ColumnsList,
        string ReadPrefix,
        string ReadAll,
        string ReadByIdId,
        string WriteSql)
        where T : IEntity<TId>
        where TId : IComparable<TId>, IEquatable<TId>
    {
        public string DeleteSqlById()
            => DeleteWhereId;

        public string DeleteSql(MatchingQuery<T, TId> matching)
            => DeletePrefix + matching.ToSql() + ";";

        public string DeleteSql(PredicateQuery<T, TId> predicate)
            => DeletePrefix + predicate.WhereClause() + ";";

        public string DeleteSql<TParam>(ParameterizedPredicateQuery<T, TId, TParam> predicate, TParam param)
            => DeletePrefix + predicate.WhereClause(param) + ";";

        public string ReadSql(MatchingQuery<T, TId> matching) =>
            ReadPrefix + matching.ToSql() + ";";

        public string ReadSql(PredicateQuery<T, TId> predicate)
            => ReadPrefix + predicate.WhereClause() + ";";

        public string ReadSql<TParam>(ParameterizedPredicateQuery<T, TId, TParam> predicate, TParam param)
            => ReadPrefix + predicate.WhereClause(param) + ";";
    }
}
