using System;

namespace Server.Entities.Common.Contracts.World;

public interface IMapTool
{
    IPathFinder PathFinder { get; }
    Func<Location.Structs.Location, Location.Structs.Location, bool, bool> SightClearChecker { get; }
}