﻿namespace Server.Entities.Common.Contracts.Creatures;

public interface IPathAccess
{
    PathFinder FindPathToDestination { get; }
    CanGoToDirection CanGoToDirection { get; }
}