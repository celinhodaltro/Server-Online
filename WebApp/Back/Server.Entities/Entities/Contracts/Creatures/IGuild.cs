using System.Collections.Generic;
using Server.Entities.Contracts.Chats;
using Server.Entities.Creatures.Guilds;

namespace Server.Entities.Contracts.Creatures;

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
    GuildRank Level { get; }
}