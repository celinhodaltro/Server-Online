using System.Linq;
using Data.Interfaces;
using Data.Parsers;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types.Containers;
using Game.Common.Contracts.Services;
using Game.Systems.Depot;
using Networking.Packets.Incoming;
using Server.BusinessRules;

namespace Server.Commands.Player.UseItem;

public class PlayerOpenDepotCommand
{
    private readonly DepotManager _depotManager;
    private readonly IItemFactory _itemFactory;
    private readonly IPlayerDepotItemRepository _playerDepotItemRepository;
    private readonly PlayerBusinessRules PlayerBusinessRules;
    private readonly IPlayerUseService _playerUseService;

    public PlayerOpenDepotCommand(IPlayerUseService playerUseService,
                                  IPlayerDepotItemRepository playerDepotItemRepository, 
                                  IItemFactory itemFactory, 
                                  DepotManager depotManager)
    {
        _playerUseService = playerUseService;
        _playerDepotItemRepository = playerDepotItemRepository;
        _itemFactory = itemFactory;
        _depotManager = depotManager;
        this.PlayerBusinessRules = PlayerBusinessRules;
    }

    public void Execute(IPlayer player, IDepot depot, UseItemPacket useItemPacket)
    {
        var playerDepot = LoadDepot(player, depot);

        playerDepot.SetNewLocation(useItemPacket.Location);

        _playerUseService.Use(player, playerDepot, useItemPacket.Index);
    }

    private IDepot LoadDepot(IPlayer player, IItem container)
    {
        var depot = _depotManager.Get(player.Id);
        if (depot is not null) return depot;

        var depotRecordsTask = this. PlayerBusinessRules.GetPlayerDepotItems(player.Id);

        depot = (IDepot)_itemFactory.Create(container.Metadata, container.Location, null);

        var depotRecords = depotRecordsTask.Result.ToList();

        var depotItemModels = depotRecords.ToList();

        ItemEntityParser.BuildContainer(depot, depotItemModels, container.Location, _itemFactory);

        _depotManager.Load(player.Id, depot);

        return depot;
    }
}