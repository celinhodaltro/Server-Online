﻿using Data.Entities;
using Game.Chats;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location.Structs;
using Game.Creatures.Player;
using Game.World;
using Loader.Players;
using Serilog;

namespace Extensions.Players.Loader;

public class TutorLoader : PlayerLoader
{
    public TutorLoader(IItemFactory itemFactory, ICreatureFactory creatureFactory,
        ChatChannelFactory chatChannelFactory, IGuildStore guildStore,
        IVocationStore vocationStore, IMapTool mapTool,
        World world, ILogger logger, GameConfiguration gameConfiguration) :
        base(itemFactory, creatureFactory, chatChannelFactory, guildStore, vocationStore, mapTool, world, logger,
            gameConfiguration)
    {
    }

    public override bool IsApplicable(Server.Entities.Character player)
    {
        return player?.PlayerType == 2;
    }

    public override IPlayer Load(Server.Entities.Character playerEntity)
    {
        if (Guard.IsNull(playerEntity)) return null;

        var town = GetTown(playerEntity);

        var newPlayer = new Tutor(
            (uint)playerEntity.Id,
            playerEntity.Name,
            VocationStore.Get(playerEntity.Vocation),
            playerEntity.Gender,
            playerEntity.Online,
            ConvertToSkills(playerEntity),
            new Outfit
            {
                Addon = (byte)playerEntity.LookAddons,
                Body = (byte)playerEntity.LookBody,
                Feet = (byte)playerEntity.LookFeet,
                Head = (byte)playerEntity.LookHead,
                Legs = (byte)playerEntity.LookLegs,
                LookType = (byte)playerEntity.LookType
            },
            playerEntity.Speed,
            new Location((ushort)playerEntity.PosX, (ushort)playerEntity.PosY, (byte)playerEntity.PosZ),
            MapTool,
            town)
        {
            AccountId = (uint)playerEntity.UserId,
            Guild = GuildStore.Get((ushort)(playerEntity.GuildMember?.GuildId ?? 0)),
            GuildLevel = (ushort)(playerEntity.GuildMember?.RankId ?? 0)
        };

        newPlayer.AddInventory(ConvertToInventory(newPlayer, playerEntity));
        SetCurrentTile(newPlayer);

        var tutor = CreatureFactory.CreatePlayer(newPlayer);
        return tutor;
    }
}