﻿using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Game.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Creatures.Guilds;
using Game.Creatures.Guild;
using Loader.Interfaces;
using Serilog;

namespace Loader.Guilds;

public class GuildLoader : ICustomLoader
{
    private readonly ChatChannelFactory _chatChannelFactory;
    private readonly IGuildStore _guildStore;
    private readonly ILogger _logger;

    public GuildLoader(ILogger logger, ChatChannelFactory chatChannelFactory, IGuildStore guildStore)
    {
        _logger = logger;
        _chatChannelFactory = chatChannelFactory;
        _guildStore = guildStore;
    }

    public void Load(Server.Entities.Guild guildEntity)
    {
        if (guildEntity is null) return;

        var guild = GetOrCreateGuild(guildEntity, out var shouldAddToStore);

        guild.Name = guildEntity.Name;
        guild.GuildLevels?.Clear();

        if ((guildEntity.Ranks?.Count ?? 0) > 0)
            guild.GuildLevels = new Dictionary<ushort, IGuildLevel>();

        AddMembers(guildEntity, guild);

        if (shouldAddToStore)
        {
            _guildStore.Add(guild.Id, guild);
            return;
        }

        _logger.Debug("Guild {Guild} loaded", guildEntity.Name);
    }

    private static void AddMembers(Server.Entities.Guild guildEntity, IGuild guild)
    {
        foreach (var memberRank in guildEntity.Members.Select(x => x.Rank))
        {
            if (memberRank is null) continue;

            var level = (GuildRank)(memberRank.Level == 0 ? (int)GuildRank.Member : memberRank.Level);
            var guildLevel = new GuildLevel(level, memberRank.Name);

            guild.GuildLevels?.Add((ushort)memberRank.Id, guildLevel);
        }
    }

    private IGuild GetOrCreateGuild(Server.Entities.Guild guildEntity, out bool shouldAddToStore)
    {
        var guild = _guildStore.Get((ushort)guildEntity.Id);

        shouldAddToStore = false;

        if (guild is not null) return guild;

        shouldAddToStore = true;

        guild = new Guild
        {
            Id = (ushort)guildEntity.Id,
            Channel = _chatChannelFactory.CreateGuildChannel($"{guildEntity.Name}'s Channel",
                (ushort)guildEntity.Id)
        };

        return guild;
    }
}