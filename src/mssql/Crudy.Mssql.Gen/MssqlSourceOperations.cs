using Crudy.Common.Gen;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Crudy.Mssql.Gen
{
    public class MssqlSourceOperations : CommonSourceOperations
    {
        public override StorageColumnDeclaration ColumnInfo(ParameterSyntax item)
        {
            var info = base.ColumnInfo(item);

            return info;
        }
    }
}
