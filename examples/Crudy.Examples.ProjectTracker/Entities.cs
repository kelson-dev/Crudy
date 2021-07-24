using Crudy.Common;
using System;
using LongText = Crudy.Common.MaxWidth<string, Crudy.Examples.ProjectTracker.Entities.S2048>;

namespace Crudy.Examples.ProjectTracker.Entities
{
    public class S128 : Size { public override uint Value => 128; }
    public class S256 : Size { public override uint Value => 256; }
    public class S2048 : Size { public override uint Value => 2048; }

    // A ColumnSet, not an Entity
    public partial record Timestamp(
        // Readonly
        // Set when a NewTimestamp is *inserted* into storage
        CreateTime<DateTimeOffset> Created,
        // Readonly
        // Set when a Timestamp is inserted or updated
        UpdateTime<DateTimeOffset> Updated,
        // Readonly
        // Set when a Timestamp is inserted or updated
        UpdateRandom<Guid> Revision)
        : ColumnSet;

    public partial record EmailContact(
        // Readonly
        // Set when an EmailContact is inserted or updated
        CreateRandom<Guid> ID,
        // Foriegn key reference to a user
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
        MaxWidth<string, S256> Title,
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
        MaxWidth<string, S128> Title,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public partial record Card(
        CreateRandom<Guid> ID,
        One<Board, Guid> Board,
        One<Status, Guid> Status,
        One<User, Guid> Author,
        One<User, Guid> Owner,
        MaxWidth<string, S128> Title,
        uint Ordinal,
        LongText Content,
        LongText? Attachement,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public partial record CardTag(
        (One<Card, Guid> Card, MaxWidth<string, S128> Tag) ID) 
        : IEntity<(One<Card, Guid> Card, string Tag)>;

    public partial record CardComment(
        Guid ID,
        One<Board, Guid> Board,
        One<Card, Guid> Card,
        One<User, Guid> Author,
        LongText Text,
        Timestamp Timestamp)
        : IEntity<Guid>;
}
