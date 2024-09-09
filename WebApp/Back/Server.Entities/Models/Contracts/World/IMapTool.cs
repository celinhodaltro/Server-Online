using Game.Common.Location.Structs;
using System;

namespace Server.Entities.Models.Contracts.World;

public interface IMapTool
{
    IPathFinder PathFinder { get; }
    Func<Location, Location, bool, bool> SightClearChecker { get; }
}