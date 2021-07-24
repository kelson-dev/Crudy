using System.Collections.Generic;

namespace Crudy.Common.Gen
{
    public class StorageColumnDeclaration
    {
        public string ClrTypeDefinition { get; set; }
        public string DbTypeDefinition { get; set; }
        public string ClrPropertyName { get; set; }
        public string DbPropertyName { get; set; }
        public bool IsNullable { get; set; } = false;
        public bool IsReference { get; set; } = false;
        public WriteStrategy CreationWriteStrategy { get; set; } = WriteStrategy.Direct;
        public WriteStrategy UpdateWriteStrategy { get; set; } = WriteStrategy.Direct;

        public List<StorageAttributeDeclaration> Attributes { get; } = new List<StorageAttributeDeclaration>();
    }
}
