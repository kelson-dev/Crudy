using Crudy.Common;
using System;

namespace Crudy.Examples.ProjectTracker.Entities
{
    public class Max256<T> : MaxWidth<T> 
    { 
        public override uint Max => 256;
        // Auto gen this?
        public static implicit operator Max256<T>(T value) => new Max256<T> { Value = value };
    }

    public class Max128<T> : MaxWidth<T>
    {
        public override uint Max => 128;
        // Auto gen this?
        public static implicit operator Max128<T>(T value) => new Max128<T> { Value = value };
    }

    public class LongText : MaxWidth<string>
    {
        public override uint Max => 2048;
        // Auto gen this?
        public static implicit operator LongText(string value) => new LongText { Value = value };
    }

    public partial record Timestamp(
        CreateTime<DateTimeOffset> Created, 
        UpdateTime<DateTimeOffset> Updated,
        UpdateRandom<Guid> Revision)
        : ColumnSet;

    public partial record EmailContact(
        CreateRandom<Guid> ID,
        One<User, Guid> User,
        string Address,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public partial record DiscordContact(
        CreateRandom<Guid> ID,
        One<User, Guid> User,
        string DiscordId,
        string DiscordJwt,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public partial record User(
        CreateRandom<Guid> ID,
        Timestamp Timestamp) 
        : IEntity<Guid>;

    public partial record Board(
        CreateRandom<Guid> ID,
        Max256<string> Title,
        One<User, Guid> Owner,
        Timestamp Timestamp)
        : IEntity<Guid>;

    [Flags]
    public enum BoardPermissions
    {
        Read = 1 << 0,
        Comment = 1 << 1,
        ManageCategories = 1 << 2,
        CreateCards = 1 << 3,
        UpdateCards = 1 << 4,
        ManageCards = 1 << 5,
    }

    public partial record BoardParticipant(
        (One<User, Guid> User, One<Board, Guid> Board) ID,
        BoardPermissions Permissions,
        Timestamp Timestamp)
        : IEntity<(One<User, Guid> User, One<Board, Guid> Board)>;

    public partial record Status(
        CreateRandom<Guid> ID,
        One<Board, Guid> Board,
        Max128<string> Title,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public partial record Card(
        CreateRandom<Guid> ID,
        One<Board, Guid> Board,
        One<Status, Guid> Status,
        One<User, Guid> Author,
        One<User, Guid> Owner,
        Max128<string> Title,
        uint Ordinal,
        LongText Content,
        LongText? Attachement,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public partial record CardTag((One<Card, Guid> Card, Max128<string> Tag) ID) : IEntity<(One<Card, Guid> Card, string Tag)>;

    public partial record CardComment(
        Guid ID,
        One<Board, Guid> Board,
        One<Card, Guid> Card,
        One<User, Guid> Author,
        LongText Text,
        Timestamp Timestamp)
        : IEntity<Guid>;
}
