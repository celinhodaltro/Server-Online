namespace Server.Entities.Models.Contracts.Creatures;

public interface IPathAccess
{
    PathFinder FindPathToDestination { get; }
    CanGoToDirection CanGoToDirection { get; }
}