﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Location;
using Networking.Packets.Incoming;
using Server.Common.Contracts;
using Server.Common.Contracts.Commands;

namespace Server.Commands.Player.UseItem;

public class PlayerUseItemOnCreatureCommand : ICommand
{
    private readonly IPlayerUseService _playerUseService;
    private readonly IGameServer game;
    private readonly HotkeyService hotKeyService;

    public PlayerUseItemOnCreatureCommand(IGameServer game, HotkeyService hotKeyCache,
        IPlayerUseService playerUseService)
    {
        this.game = game;
        hotKeyService = hotKeyCache;
        _playerUseService = playerUseService;
    }

    public void Execute(IPlayer player, UseItemOnCreaturePacket useItemPacket)
    {
        if (!game.CreatureManager.TryGetCreature(useItemPacket.CreatureId, out var creature)) return;

        var itemToUse = GetItem(player, useItemPacket);

        if (itemToUse is not IUsableOn useableOn) return;

        var action = () => _playerUseService.Use(player, useableOn, creature);

        if (useItemPacket.FromLocation.Type == LocationType.Ground)
        {
            action();
            return;
        }

        action();
    }

    private IThing GetItem(IPlayer player, UseItemOnCreaturePacket useItemPacket)
    {
        if (useItemPacket.FromLocation.IsHotkey) return hotKeyService.GetItem(player, useItemPacket.ClientId);

        if (useItemPacket.FromLocation.Type == LocationType.Ground)
        {
            if (game.Map[useItemPacket.FromLocation] is not { } tile) return null;
            return tile.TopItemOnStack;
        }

        if (useItemPacket.FromLocation.Type == LocationType.Slot)
        {
            if (player.Inventory[useItemPacket.FromLocation.Slot] is null) return null;
            return player.Inventory[useItemPacket.FromLocation.Slot];
        }

        if (useItemPacket.FromLocation.Type == LocationType.Container)
        {
            if (player.Containers[useItemPacket.FromLocation.ContainerId][useItemPacket.FromLocation.ContainerSlot]
                is not IThing thing) return null;
            return thing;
        }

        return null;
    }
}