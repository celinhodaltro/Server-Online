﻿using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Location;
using Networking.Packets.Incoming;
using Server.Common.Contracts.Commands;

namespace Server.Commands.Player.UseItem;

public class PlayerUseItemCommand : ICommand
{
    private readonly ItemFinderService _itemFinderService;
    private readonly PlayerOpenDepotCommand _playerOpenDepotCommand;
    private readonly IPlayerUseService _playerUseService;

    public PlayerUseItemCommand(IPlayerUseService playerUseService, PlayerOpenDepotCommand playerOpenDepotCommand,
        ItemFinderService itemFinderService)
    {
        _playerUseService = playerUseService;
        _playerOpenDepotCommand = playerOpenDepotCommand;
        _itemFinderService = itemFinderService;
    }

    public void Execute(IPlayer player, UseItemPacket useItemPacket)
    {
        var item = _itemFinderService.Find(player, useItemPacket.Location, useItemPacket.ClientId);

        Action action;

        switch (item)
        {
            case null:
                return;
            case IDepot depot:
                action = () => _playerOpenDepotCommand.Execute(player, depot, useItemPacket);
                break;
            case IContainer container:
                action = () => _playerUseService.Use(player, container, useItemPacket.Index);
                break;
            case IUsableOn usableOn:
                action = () => _playerUseService.Use(player, usableOn, player);
                break;
            default:
                action = () => _playerUseService.Use(player, item);
                break;
        }

        if (useItemPacket.Location.Type == LocationType.Ground)
        {
            action();
            return;
        }

        action();
    }
}