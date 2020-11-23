using Crudy.Common.Gen;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Crudy.Mssql.Gen
{
    [Generator]
    public class SqlGenerator : ISourceGenerator
    {
        private CommonSourceOperations ops = new MssqlSourceOperations();

        public void Execute(GeneratorExecutionContext context)
        {
            Debug.WriteLine("Executing code generator");
            var compilation = context.Compilation;
            var trees = compilation.SyntaxTrees.ToImmutableArray();

            var nodes = compilation.SyntaxTrees.SelectMany(tree => tree.GetRoot().DescendantNodes());

            var records = nodes
                .WhereIs<SyntaxNode, RecordDeclarationSyntax>()
                .ToImmutableArray();
                
            var recordEntities = records
                .Where(ops.IsEntityDeclarationRecord)
                .ToImmutableArray();

            var classes = nodes
                .WhereIs<SyntaxNode, ClassDeclarationSyntax>()
                .ToImmutableArray();

            var classEntities = classes
                .Where(ops.IsEntityDeclarationClass)
                .ToImmutableArray();

            var structs = nodes
                .WhereIs<SyntaxNode, StructDeclarationSyntax>()
                .ToImmutableArray();

            var structEntities = structs
                .Where(ops.IsEntityDeclarationStruct)
                .ToImmutableArray();

            Debug.WriteLine($"Found {records.Length} record declarations");
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
#endif 
        }
    }
}
