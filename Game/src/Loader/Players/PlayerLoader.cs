using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Data.Entities;
using Data.Parsers;
using Game.Chats;
using Game.Combat.Conditions;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types.Containers;
using Game.Common.Contracts.World;
using Game.Common.Contracts.World.Tiles;
using Game.Common.Creatures;
using Game.Common.Creatures.Players;
using Game.Common.Helpers;
using Game.Common.Item;
using Game.Common.Location.Structs;
using Game.Creatures.Player;
using Game.Creatures.Player.Inventory;
using Loader.Interfaces;
using Serilog;

namespace Loader.Players;

public class PlayerLoader
{
    private readonly GameConfiguration _gameConfiguration;
    protected readonly ChatChannelFactory ChatChannelFactory;
    protected readonly ICreatureFactory CreatureFactory;
    protected readonly IGuildStore GuildStore;
    protected readonly IItemFactory ItemFactory;
    protected readonly ILogger Logger;
    protected readonly IMapTool MapTool;
    protected readonly IVocationStore VocationStore;
    protected readonly Game.World.World World;

    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public PlayerLoader(IItemFactory itemFactory, ICreatureFactory creatureFactory,
        ChatChannelFactory chatChannelFactory,
        IGuildStore guildStore,
        IVocationStore vocationStore,
        IMapTool mapTool,
        Game.World.World world,
        ILogger logger,
        GameConfiguration gameConfiguration)
    {
        ItemFactory = itemFactory;
        CreatureFactory = creatureFactory;
        ChatChannelFactory = chatChannelFactory;
        GuildStore = guildStore;
        VocationStore = vocationStore;
        MapTool = mapTool;
        World = world;
        Logger = logger;
        _gameConfiguration = gameConfiguration;
    }

    public virtual bool IsApplicable(Server.Entities.Character player)
    {
        return player?.PlayerType == 1;
    }

    public virtual IPlayer Load(Server.Entities.Character playerEntity)
    {
        if (Guard.IsNull(playerEntity)) return null;

        var vocation = GetVocation(playerEntity);
        var town = GetTown(playerEntity);

        var playerLocation =
            new Location((ushort)playerEntity.PosX, (ushort)playerEntity.PosY, (byte)playerEntity.PosZ);

        var player = new Player(
            (uint)playerEntity.Id,
            playerEntity.Name,
            playerEntity.ChaseMode,
            playerEntity.Capacity,
            playerEntity.Health,
            playerEntity.MaxHealth,
            vocation,
            playerEntity.Gender,
            playerEntity.Online,
            playerEntity.Mana,
            playerEntity.MaxMana,
            playerEntity.FightMode,
            playerEntity.Soul,
            vocation.SoulMax,
            ConvertToSkills(playerEntity),
            playerEntity.StaminaMinutes,
            new Outfit
            {
                Addon = (byte)playerEntity.LookAddons,
                Body = (byte)playerEntity.LookBody,
                Feet = (byte)playerEntity.LookFeet,
                Head = (byte)playerEntity.LookHead,
                Legs = (byte)playerEntity.LookLegs,
                LookType = (byte)playerEntity.LookType
            },
            0,
            playerLocation,
            MapTool,
            town)
        {
            PremiumTime = playerEntity.Account?.UserInfo.PremiumTime ?? 0,
            AccountId = (uint)playerEntity.UserId,
            Guild = GuildStore.Get((ushort)(playerEntity.GuildMember?.GuildId ?? 0)),
            GuildLevel = (ushort)(playerEntity.GuildMember?.RankId ?? 0)
        };

        SetCurrentTile(player);
        AddRegenerationCondition(playerEntity, player);

        player.AddInventory(ConvertToInventory(player, playerEntity));

        AddExistingPersonalChannels(player);

        return CreatureFactory.CreatePlayer(player);
    }

    protected ITown GetTown(Server.Entities.Character playerEntity)
    {
        if (!World.TryGetTown((ushort)playerEntity.TownId, out var town))
            Logger.Error("player town not found: {PlayerModelTownId}", playerEntity.TownId);
        return town;
    }

    protected IVocation GetVocation(Server.Entities.Character playerEntity)
    {
        if (!VocationStore.TryGetValue(playerEntity.Vocation, out var vocation))
            Logger.Error("Player vocation not found: {PlayerModelVocation}", playerEntity.Vocation);
        return vocation;
    }

    protected void SetCurrentTile(IPlayer player)
    {
        var location = player.Location;

        var playerTile = World.TryGetTile(ref location, out var tile) && tile is IDynamicTile dynamicTile
            ? dynamicTile
            : null;

        if (playerTile is not null)
        {
            player.SetCurrentTile(playerTile);
            return;
        }

        var townLocation = player.Town.Coordinate.Location;

        playerTile = World.TryGetTile(ref townLocation, out var townTile) && townTile is IDynamicTile townDynamicTile
            ? townDynamicTile
            : null;

        player.SetCurrentTile(playerTile);
    }

    private static void AddRegenerationCondition(Server.Entities.Character playerEntity, IPlayer player)
    {
        if (playerEntity.RemainingRecoverySeconds != 0)
        {
            player.AddCondition(
                new Condition(ConditionType.Regeneration, (uint)(playerEntity.RemainingRecoverySeconds * 1000),
                    player.SetAsHungry));
            return;
        }

        player.SetAsHungry();
    }

    /// <summary>
    ///     Adds all PersonalChatChannel assemblies to Player
    /// </summary>
    protected virtual void AddExistingPersonalChannels(IPlayer player)
    {
        if (player is null) return;

        var personalChannels = GameAssemblyCache.Cache
            .Where(x => typeof(PersonalChatChannel).IsAssignableFrom(x));
        foreach (var channel in personalChannels)
        {
            if (channel == typeof(PersonalChatChannel)) continue;

            var createdChannel = ChatChannelFactory.Create(channel, null, player);
            player.Channels.AddPersonalChannel(createdChannel);
        }
    }

    protected Dictionary<SkillType, ISkill> ConvertToSkills(Server.Entities.Character playerRecord)
    {
        return new Dictionary<SkillType, ISkill>
        {
            [SkillType.Axe] = new Skill(SkillType.Axe, (ushort)playerRecord.SkillAxe, playerRecord.SkillAxeTries)
                { GetIncreaseRate = () => _gameConfiguration.SkillsRate["axe"] },

            [SkillType.Club] = new Skill(SkillType.Club, (ushort)playerRecord.SkillClub, playerRecord.SkillClubTries)
                { GetIncreaseRate = () => _gameConfiguration.SkillsRate["club"] },

            [SkillType.Distance] = new Skill(SkillType.Distance, (ushort)playerRecord.SkillDist,
                    playerRecord.SkillDistTries)
                { GetIncreaseRate = () => _gameConfiguration.SkillsRate["distance"] },

            [SkillType.Fishing] = new Skill(SkillType.Fishing, (ushort)playerRecord.SkillFishing,
                    playerRecord.SkillFishingTries)
                { GetIncreaseRate = () => _gameConfiguration.SkillsRate["fishing"] },

            [SkillType.Fist] = new Skill(SkillType.Fist, (ushort)playerRecord.SkillFist, playerRecord.SkillFistTries)
                { GetIncreaseRate = () => _gameConfiguration.SkillsRate["fist"] },

            [SkillType.Shielding] = new Skill(SkillType.Shielding, (ushort)playerRecord.SkillShielding,
                    playerRecord.SkillShieldingTries)
                { GetIncreaseRate = () => _gameConfiguration.SkillsRate["shielding"] },

            [SkillType.Level] = new Skill(SkillType.Level, playerRecord.Level, playerRecord.Experience),

            [SkillType.Magic] =
                new Skill(SkillType.Magic, (ushort)playerRecord.MagicLevel, playerRecord.MagicLevelTries)
                    { GetIncreaseRate = () => _gameConfiguration.SkillsRate["magic"] },

            [SkillType.Sword] =
                new Skill(SkillType.Sword, (ushort)playerRecord.SkillSword, playerRecord.SkillSwordTries)
                    { GetIncreaseRate = () => _gameConfiguration.SkillsRate["sword"] }
        };
    }

    protected IInventory ConvertToInventory(IPlayer player, Server.Entities.Character playerRecord)
    {
        var inventory = new Dictionary<Slot, (IItem Item, ushort Id)>();
        var attrs = new Dictionary<ItemAttribute, IConvertible> { { ItemAttribute.Count, 0 } };

        foreach (var item in playerRecord.PlayerInventoryItems)
        {
            attrs[ItemAttribute.Count] = (byte)item.Amount;
            var location = item.SlotId <= 10 ? Location.Inventory((Slot)item.SlotId) : Location.Container(0, 0);

            var createdItem = ItemFactory.Create((ushort)item.ServerId, location, attrs);
            var createdItemIsPickupable = createdItem?.IsPickupable ?? false;

            if (!createdItemIsPickupable) continue;

            if (item.SlotId == (int)Slot.Backpack)
            {
                if (createdItem is not IContainer container) continue;

                ItemEntityParser.BuildContainer(container, playerRecord.PlayerItems.ToList(), location, ItemFactory);
            }

            inventory.Add((Slot)item.SlotId, (createdItem, (ushort)item.ServerId));
        }

        return new Inventory(player, inventory);
    }
}