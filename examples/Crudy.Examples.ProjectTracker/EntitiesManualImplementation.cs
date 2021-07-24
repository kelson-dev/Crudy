using Crudy.Common;
using Crudy.Common.Sql;
using Crudy.Mssql.Common;
using System;
using System.Linq;

namespace Crudy.Examples.ProjectTracker.Entities
{
    /// <summary>
    /// Not a table entity, has no ID
    /// Will exist as column tuples
    /// </summary>
    public partial record Timestamp
    {
        public static Timestamp New() => new(new(), new(), new());
    }

    public partial record EmailContact
    {
        public static EmailContact New(Guid userId, string address) => new(
            ID: new(),
            User: userId,
            Address: address,
            Timestamp: Timestamp.New());

        public IReadWriteEntityStorage<EmailContact, Guid>? Storage { get; set; }

        private static readonly string _tableName = nameof(EmailContact).PluralizeCamelCase();
        public string TableName => _tableName;

        private static readonly Column _idColumn = 
            new Column(typeof(CreateRandom<Guid>), nameof(ID));
        
        private static readonly Column[] _columns = new Column[]
        {
            new Column(typeof(One<User, Guid>), nameof(User)),
            new Column(typeof(string), nameof(Address)),
            new Column(typeof(Timestamp), nameof(Timestamp))
        };

        public static readonly MssqlQueryCollection<EmailContact, Guid> Queries = MssqlQueryCollection<EmailContact, Guid>.Create(_tableName, _idColumn, _columns);
    }

    public partial record User
    {
        public static User New() => new(new(), Timestamp.New());

        private static readonly string _tableName = nameof(User).PluralizeCamelCase();
        public string TableName => _tableName;

        private static readonly Column _idColumn = 
            new Column(typeof(CreateRandom<Guid>), nameof(ID));

        private static readonly Column[] _columns = new Column[]
        {
            new Column(typeof(Timestamp), nameof(Timestamp))
        };

        public static readonly MssqlQueryCollection<User, Guid> Queries = MssqlQueryCollection<User, Guid>.Create(_tableName, _idColumn, _columns);
    }

    public partial record Board
    {
        public static Board New(string title, Guid ownerId) => new(
            ID: new(),
            Title: title,
            Owner: ownerId,
            Timestamp.New());

        private static readonly string _tableName = nameof(Board).PluralizeCamelCase();
        public string TableName => _tableName;

        private static readonly Column _idColumn =
            new Column(typeof(CreateRandom<Guid>), nameof(ID));

        private static readonly Column[] _columns = new Column[]
        {
            new Column(typeof(MaxWidth<string, S256>), nameof(Title)),
            new Column(typeof(Timestamp), nameof(Timestamp))
        };

        public static readonly MssqlQueryCollection<Board, Guid> Queries = MssqlQueryCollection<Board, Guid>.Create(_tableName, _idColumn, _columns);
            
    }
}
