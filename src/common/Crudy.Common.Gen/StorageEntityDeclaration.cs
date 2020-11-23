namespace Crudy.Common.Gen
{
    public class StorageEntityDeclaration
    {
        public string EntityName { get; set; }
        public string TypeName { get; set; }
        public StorageColumnDeclaration[] Columns { get; set; }
    }
}
