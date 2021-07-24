using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Crudy.Common.Gen
{
    public class CommonSourceOperations
    {
        private static IEnumerable<string> BaseTypeTexts(TypeDeclarationSyntax type) =>
            type.BaseList?.Types.Select(t => t.GetText().ToString()) ?? Enumerable.Empty<string>();

        public virtual bool IsColumnSetRecord(TypeDeclarationSyntax type)
        {
            if (!type.Modifiers.Any(ArePartial))
                return false;
            return BaseTypeTexts(type).Any(t => t.StartsWith("ColumnSet"));
        }

        public string InnerColumnTypeString(TypeSyntax type)
        {
            throw new NotImplementedException();
            if (type is TupleTypeSyntax tuple)
                throw new NotImplementedException();
            //else if (type is Generic)
            
            return type.ToString();
        }

        public virtual bool IsEntityDeclarationRecord(RecordDeclarationSyntax type)
        {
            if (!type.Modifiers.Any(ArePartial))
                return false;

            ParameterSyntax? firstParam = type.ParameterList?.Parameters.FirstOrDefault();
            if (firstParam?.Identifier.ToString() != "ID")
                return false;
            var idTypeString = InnerColumnTypeString(firstParam.Type);
            var isEntity = (type.BaseList?.Types.Select(t => t.GetText().ToString()) ?? Enumerable.Empty<string>())
                .Any(t => t.StartsWith($"IEntity<{idTypeString}>"));
            return isEntity;
        }       

        public virtual bool IsEntityDeclarationClass(ClassDeclarationSyntax type)
        {
            if (!type.Modifiers.Any(ArePartial))
                return false;

            var baseTypeTexts = (type.BaseList?.Types.Select(t => t.GetText().ToString()) ?? Enumerable.Empty<string>()).ToImmutableArray();

            if (type.Members.WhereIs<MemberDeclarationSyntax, PropertyDeclarationSyntax>()
                .Take(1)
                .FirstOrDefault(p => p.Identifier.ToString() == "ID") 
                is PropertyDeclarationSyntax idProperty)
            {
                var idTypeString = idProperty.Type?.ToString();
                var isEntity = baseTypeTexts.Any(t => t.StartsWith($"IEntity<{idTypeString}>"));
                return isEntity;
            }
            else if (type.Members.WhereIs<MemberDeclarationSyntax, FieldDeclarationSyntax>()
                .Take(1)
                .FirstOrDefault(p => p.Declaration.Variables.First().Identifier.ToString() == "ID")
                is FieldDeclarationSyntax idField)
            {
                var idTypeString = idField.Declaration.Type?.ToString();
                var isEntity = baseTypeTexts.Any(t => t.StartsWith($"IEntity<{idTypeString}>"));
                return isEntity;
            }
            else
            {
                return false;
            }
        }
        
        public virtual StorageAttributeDeclaration ToStorageAttribute(AttributeSyntax attribute)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsEntityDeclarationStruct(StructDeclarationSyntax type)
        {
            throw new NotImplementedException();
        }

        public virtual StorageColumnDeclaration ColumnInfo(ParameterSyntax item)
        {
            var attrs = item.AttributeLists.SelectMany(attrList => attrList.Attributes);

            throw new NotImplementedException();
        }

        public virtual StorageColumnDeclaration ColumnInfo(PropertyDeclarationSyntax item)
        {
            throw new NotImplementedException();
        }

        public virtual StorageColumnDeclaration ColumnInfo(FieldDeclarationSyntax item)
        {
            throw new NotImplementedException();
        }

        public virtual StorageEntityDeclaration EntityInfo(RecordDeclarationSyntax item, Dictionary<string, StorageEntityDeclaration> columnSets)
        {
            throw new NotImplementedException();
        }

        public virtual StorageEntityDeclaration EntityInfo(ClassDeclarationSyntax item, Dictionary<string, StorageEntityDeclaration> columnSets)
        {
            throw new NotImplementedException();
        }

        public virtual StorageEntityDeclaration EntityInfo(StructDeclarationSyntax item, Dictionary<string, StorageEntityDeclaration> columnSets)
        {
            throw new NotImplementedException();
        }

        private static readonly Func<SyntaxToken, bool> ArePartial = token => token.IsKind(SyntaxKind.PartialKeyword);
    }
}
