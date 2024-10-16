using System;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Location.Structs;
using Game.World.Algorithms;

namespace Game.World.Services;

public class MapTool : IMapTool
{
    public MapTool(IMap map, IPathFinder pathFinder)
    {
        PathFinder = pathFinder;
        SightClearChecker = (from, to, checkFloor) =>
            SightClear.IsSightClear(map, from, to, checkFloor);
    }

    public IPathFinder PathFinder { get; }
    public Func<Location, Location, bool, bool> SightClearChecker { get; }
}