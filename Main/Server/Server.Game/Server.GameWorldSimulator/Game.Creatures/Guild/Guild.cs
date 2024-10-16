﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Guilds;

namespace Game.Creatures.Guild;

public class Guild : IGuild
{
    public ushort Id { get; init; }
    public string Name { get; set; }
    public IDictionary<ushort, IGuildLevel> GuildLevels { get; set; }
    public IChatChannel Channel { get; set; }

    public bool HasMember(IPlayer player)
    {
        return player.GuildId == Id;
    }

    public IGuildLevel GetMemberLevel(IPlayer player)
    {
        return GuildLevels is null ? null : GuildLevels.TryGetValue(player.Level, out var level) ? level : null;
    }

    public string InspectionText(IPlayer player)
    {
        return $"{player.GenderPronoun} is member of the {Name}.";
    }
}

public class GuildLevel : IGuildLevel, IEquatable<GuildLevel>
{
    private string levelName;

    public GuildLevel(GuildRank level = GuildRank.Member, string levelName = null)
    {
        Level = level;
        LevelName = levelName;
    }

    public ushort Id { get; init; }

    public string LevelName
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(levelName)) return levelName;
            return Level switch
            {
                GuildRank.Leader => "Leader",
                GuildRank.ViceLeader => "Vice-Leader",
                _ => "Member"
            };
        }
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            levelName = value;
        }
    }

    public bool Equals(GuildLevel other)
    {
        return other.Id == Id;
    }

    public GuildRank Level { get; }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}