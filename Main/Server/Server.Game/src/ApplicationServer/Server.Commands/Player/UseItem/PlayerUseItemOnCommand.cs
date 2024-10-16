﻿using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Networking.Packets.Incoming;
using Server.Common.Contracts;
using Server.Common.Contracts.Commands;

namespace Server.Commands.Player.UseItem;

public class PlayerUseItemOnCommand : ICommand
{
    private readonly IPlayerUseService _playerUseService;
    private readonly IGameServer game;
    private readonly HotkeyService hotkeyService;

    public PlayerUseItemOnCommand(IGameServer game, HotkeyService hotkeyService, IPlayerUseService playerUseService)
    {
        this.game = game;
        this.hotkeyService = hotkeyService;
        _playerUseService = playerUseService;
    }

    public void Execute(IPlayer player, UseItemOnPacket useItemPacket)
    {
        IItem onItem = null;
        ITile onTile = null;

        if (useItemPacket.ToLocation.Type == LocationType.Ground)
        {
            if (game.Map[useItemPacket.ToLocation] is not { } tile) return;
            onTile = tile;
        }

        if (useItemPacket.ToLocation.Type == LocationType.Slot)
        {
            if (player.Inventory[useItemPacket.ToLocation.Slot] is null) return;
            onItem = player.Inventory[useItemPacket.ToLocation.Slot];
        }

        if (useItemPacket.ToLocation.Type == LocationType.Container)
        {
            if (player.Containers[useItemPacket.ToLocation.ContainerId][useItemPacket.ToLocation.ContainerSlot] is
                not { } item) return;
            onItem = item;
        }

        if (onItem is null && onTile is null) return;

        IThing thingToUse = null;

        if (useItemPacket.Location.IsHotkey)
            thingToUse = hotkeyService.GetItem(player, useItemPacket.ClientId);

        else
            switch (useItemPacket.Location.Type)
            {
                case LocationType.Ground:
                {
                    if (game.Map[useItemPacket.Location] is not { } tile) return;
                    thingToUse = tile.TopItemOnStack;
                    break;
                }
                case LocationType.Slot:
                    thingToUse = player.Inventory[useItemPacket.Location.Slot];
                    break;
                case LocationType.Container:
                    thingToUse =
                        player.Containers[useItemPacket.Location.ContainerId][useItemPacket.Location.ContainerSlot];
                    break;
            }

        if (thingToUse is not IUsableOn itemToUse) return;

        Action action = onTile is not null
            ? () => _playerUseService.Use(player, itemToUse, onTile)
            : () => _playerUseService.Use(player, itemToUse, onItem);

        if (useItemPacket.Location.Type == LocationType.Ground)
        {
            action();
            return;
        }

        action();
    }
}