﻿using System;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Game.Creatures.Npcs;
using Game.Creatures.Npcs.Shop;
using Game.Creatures.Player;
using Serilog;

namespace Game.Creatures.Factories;

public class NpcFactory : INpcFactory
{
    private readonly ICoinTypeStore _coinTypeStore;
    private readonly IItemFactory _itemFactory;
    private readonly ILogger _logger;
    private readonly IMapTool _mapTool;
    private readonly INpcStore _npcStore;

    public NpcFactory(
        ILogger logger, IItemFactory itemFactory,
        INpcStore npcStore, ICoinTypeStore coinTypeStore, IMapTool mapTool)
    {
        _logger = logger;
        _itemFactory = itemFactory;
        _npcStore = npcStore;
        _coinTypeStore = coinTypeStore;
        _mapTool = mapTool;
        Instance = this;
    }

    public static INpcFactory Instance { get; private set; }

    public INpc Create(string name, ISpawnPoint spawn = null)
    {
        var npcType = _npcStore.Get(name);
        if (npcType is null)
        {
            _logger.Warning($"Given npc name: {name} is not loaded");
            return null;
        }

        var outfit = BuildOutfit(npcType);

        if (npcType.CustomAttributes?.ContainsKey("shop") ?? false)
            return new ShopperNpc(npcType, _mapTool, spawn, outfit, npcType.MaxHealth)
            {
                CreateNewItem = _itemFactory.Create,
                CoinTypeMapFunc = () => _coinTypeStore.Map
            };

        if (npcType.IsCSharpScript)
        {
            var className = npcType.Script.Replace(".cs", string.Empty);

            var type = GameAssemblyCache.Cache
                .FirstOrDefault(x => x.Name.Equals(className) || (x.FullName?.Equals(className) ?? false));

            if (type is not null &&
                Activator.CreateInstance(type, npcType, _mapTool, spawn, outfit, npcType.MaxHealth) is INpc instance)
                return instance;
        }

        return new Npc(npcType, _mapTool, spawn, outfit, npcType.MaxHealth);
    }

    private static Outfit BuildOutfit(INpcType npcType)
    {
        if (npcType?.Look is null) return new Outfit();

        return new Outfit
        {
            Addon = npcType.Look.TryGetValue(LookType.Addon, out var addon) ? (byte)addon : default,
            LookType = npcType.Look.TryGetValue(LookType.Type, out var type) ? (byte)type : default,
            Body = npcType.Look.TryGetValue(LookType.Body, out var body) ? (byte)body : default,
            Feet = npcType.Look.TryGetValue(LookType.Feet, out var feet) ? (byte)feet : default,
            Head = npcType.Look.TryGetValue(LookType.Head, out var head) ? (byte)head : default,
            Legs = npcType.Look.TryGetValue(LookType.Legs, out var legs) ? (byte)legs : default
        };
    }
}