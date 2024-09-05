using System.Collections.Generic;
using Game.Common.Contracts.Chats;
using Game.Common.Creatures.Guilds;

namespace Game.Common.Contracts.Creatures;

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