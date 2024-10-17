using Server.Entities.Common.Contracts.Chats;


namespace Server.Entities.Common.Contracts.Creatures;

public interface IGuild
{
    ushort Id { get; init; }
    string Name { get; set; }
    IDictionary<ushort, IGuildLevel> GuildLevels { get; set; }
    IChatChannel Channel { get; set; }

    IGuildLevel GetMemberLevel(IPlayer player);
    bool HasMember(IPlayer player);
    string InspectionText(IPlayer player);
}

public interface IGuildLevel
{
    Server.Entities.Common.Creatures.Guilds.GuildRank Level { get; }
}