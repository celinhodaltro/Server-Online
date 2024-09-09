using System;
using Server.Entities.Models.Contracts.World;
using Server.Entities.Models.Location.Structs;
using Game.World.Algorithms;

namespace Server.Entities.Models.World;
public class MapTool : IMapTool
{
    public MapTool(IMap map, IPathFinder pathFinder)
    {
        PathFinder = pathFinder;
        SightClearChecker = (from, to, checkFloor) =>
            SightClear.IsSightClear(map, from, to, checkFloor);
    }

    public IPathFinder PathFinder { get; }
    public Func<Location.Structs.Location, Location.Structs.Location, bool, bool> SightClearChecker { get; }
}