using Crudy.Common.Gen;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Crudy.Mssql.Gen
{
    public class CompilationOrganization
    {
        public ImmutableArray<TypeDeclarationSyntax> Types { get; set; }
        public SortedList<string, TypeDeclarationSyntax> ColumnSets { get; set; }
        public ImmutableArray<RecordDeclarationSyntax> RecordEntities { get; set; }
        public ImmutableArray<ClassDeclarationSyntax> ClassEntities { get; set; }
        public ImmutableArray<StructDeclarationSyntax> StructEntities { get; set; }
    }

    [Generator]
    public class SqlGenerator : ISourceGenerator
    {
        private CommonSourceOperations ops = new MssqlSourceOperations();

        public void Execute(GeneratorExecutionContext context)
        {
            return;
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

                var definition = new CompilationOrganization
                {
                    Types = nodes,
                    ColumnSets = nodes
                        .Where(ops.IsColumnSetRecord)
                        .ToSortedList(t => t.Identifier.ValueText),
                    RecordEntities = nodes
                        .WhereIs<SyntaxNode, RecordDeclarationSyntax>()
                        .Where(ops.IsEntityDeclarationRecord)
                        .ToImmutableArray(),
                    ClassEntities = nodes
                        .WhereIs<SyntaxNode, ClassDeclarationSyntax>()
                        .Where(ops.IsEntityDeclarationClass)
                        .ToImmutableArray(),
                    StructEntities = nodes
                        .WhereIs<SyntaxNode, StructDeclarationSyntax>()
                        .Where(ops.IsEntityDeclarationStruct)
                        .ToImmutableArray()
                };

                Debug.WriteLine($"Found {definition.Types.Length} type declarations");
                Debug.WriteLine($"Found {definition.ColumnSets.Count} column set declarations");
                Debug.WriteLine($"Found {definition.RecordEntities.Length} record declarations");
                Debug.WriteLine($"Found {definition.ClassEntities.Length} class declarations");
                Debug.WriteLine($"Found {definition.StructEntities.Length} struct declarations");
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //var fileSaysYes = File.ReadAllLines(@"C:\Users\kelso\Desktop\environment.txt")
            //    .Any(line => line.StartsWith("debugSourceGen") && line.EndsWith("yes"));

            //if (!Debugger.IsAttached && fileSaysYes)
            //    Debugger.Launch();
        }
    }
}
