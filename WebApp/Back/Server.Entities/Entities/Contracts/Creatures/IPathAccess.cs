namespace Server.Entities.Contracts.Creatures;

public interface IPathAccess
{
    PathFinder FindPathToDestination { get; }
    CanGoToDirection CanGoToDirection { get; }
}