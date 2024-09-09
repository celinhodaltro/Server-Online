using Server.Entities.Models.Location.Structs;
using System;

namespace Server.Entities.Models.Contracts.World;

public interface IMapTool
{
    IPathFinder PathFinder { get; }
    Func<Location.Structs.Location, Location.Structs.Location, bool, bool> SightClearChecker { get; }
}