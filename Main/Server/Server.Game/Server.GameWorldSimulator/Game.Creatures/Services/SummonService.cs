﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Game.Creatures.Monster.Summon;
using Serilog;

namespace Game.Creatures.Services;

public class SummonService : ISummonService
{
    private readonly ICreatureFactory _creatureFactory;
    private readonly ILogger _logger;
    private readonly IMap _map;

    public SummonService(ICreatureFactory creatureFactory, IMap map, ILogger logger)
    {
        _creatureFactory = creatureFactory;
        _map = map;
        _logger = logger;
    }

    public IMonster Summon(IMonster master, string summonName)
    {
        if (_creatureFactory.CreateSummon(summonName, master) is not Summon summon)
        {
            _logger.Error($"Summon with name: {summonName} does not exists");
            return null;
        }

        foreach (var neighbour in master.Location.Neighbours)
            if (_map[neighbour] is IDynamicTile { HasCreature: false } toTile &&
                !toTile.HasFlag(TileFlags.Unpassable) && !toTile.HasTeleport(out _))
            {
                summon.Born(toTile.Location);
                return summon;
            }

        return null;
    }
}