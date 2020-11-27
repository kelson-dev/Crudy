using Crudy.Common.Gen;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Crudy.Mssql.Gen
{
    public record CompilationOrganization(
        ImmutableArray<TypeDeclarationSyntax> Types,
        SortedList<string, TypeDeclarationSyntax> ColumnSets,
        ImmutableArray<RecordDeclarationSyntax> RecordEntities,
        ImmutableArray<ClassDeclarationSyntax> ClassEntities,
        ImmutableArray<StructDeclarationSyntax> StructEntities);

    [Generator]
    public class SqlGenerator : ISourceGenerator
    {
        private CommonSourceOperations ops = new MssqlSourceOperations();

        public void Execute(GeneratorExecutionContext context)
        {
            Debug.WriteLine("Executing code generator");
            if (context.Compilation is Compilation compilation)
            {
                var nodes = compilation
                    .SyntaxTrees
                    .SelectMany(tree => tree
                        .GetRoot()
                        .DescendantNodes()
                        .WhereIs<SyntaxNode, TypeDeclarationSyntax>())
                    .ToImmutableArray();

                CompilationOrganization definition = new(
                    Types: nodes,
                    ColumnSets: nodes
                        .Where(ops.IsColumnSetRecord)
                        .ToSortedList<string, TypeDeclarationSyntax>(t => t.Identifier.ValueText),
                    RecordEntities: nodes
                        .WhereIs<SyntaxNode, RecordDeclarationSyntax>()
                        .Where(ops.IsEntityDeclarationRecord)
                        .ToImmutableArray(),
                    ClassEntities: nodes
                        .WhereIs<SyntaxNode, ClassDeclarationSyntax>()
                        .Where(ops.IsEntityDeclarationClass)
                        .ToImmutableArray(),
                    StructEntities: nodes
                        .WhereIs<SyntaxNode, StructDeclarationSyntax>()
                        .Where(ops.IsEntityDeclarationStruct)
                        .ToImmutableArray()
                    );

                Debug.WriteLine($"Found {definition.Types.Length} type declarations");
                Debug.WriteLine($"Found {definition.ColumnSets.Count} column set declarations");
                Debug.WriteLine($"Found {definition.RecordEntities.Length} record declarations");
                Debug.WriteLine($"Found {definition.ClassEntities.Length} class declarations");
                Debug.WriteLine($"Found {definition.StructEntities.Length} struct declarations");
            }
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
