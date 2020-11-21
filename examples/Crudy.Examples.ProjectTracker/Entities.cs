using Crudy.Common;
using System;

namespace Crudy.Examples.ProjectTracker.Entities
{
    public record Timestamp(
        [Created] DateTimeOffset Created, 
        [Updated] DateTimeOffset Updated,
        [UpdateRandom] Guid Revision);

    public record EmailContact(
        Guid ID,
        One<User, Guid> User, 
        string Address,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public record DiscordContact(
        Guid ID,
        One<User, Guid> User,
        string DiscordId,
        string DiscordJwt,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public record User(
        Guid ID,
        Timestamp Timestamp) 
        : IEntity<Guid>;

    public record Board(
        Guid ID,
        [MaxWidth(256)] string Title,
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

    public record BoardParticipant(
        [Explicit] (One<User, Guid> User, One<Board, Guid> Board) ID,
        BoardPermissions Permissions,
        Timestamp Timestamp)
        : IEntity<(One<User, Guid> User, One<Board, Guid> Board)>;

    public record Status(
        Guid ID,
        One<Board, Guid> Board,
        [MaxWidth(64)] string Title,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public record Card(
        Guid ID,
        One<Board, Guid> Board,
        One<Status, Guid> Status,
        One<User, Guid> Author,
        One<User, Guid> Owner,
        [MaxWidth(128)] string Title,
        uint Ordinal,
        string Content,
        string? Attachement,
        Timestamp Timestamp)
        : IEntity<Guid>;

    public record CardTag([Explicit] (One<Card, Guid> Card, string Tag) ID) : IEntity<(One<Card, Guid> Card, string Tag)>;

    public record CardComment(
        Guid ID,
        One<Board, Guid> Board,
        One<Card, Guid> Card,
        One<User, Guid> Author,
        [MaxWidth(2048)] string Text,
        Timestamp Timestamp)
        : IEntity<Guid>;
}
