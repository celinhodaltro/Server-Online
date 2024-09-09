namespace Server.Entities.Models.Location;

public enum Direction : byte
{
    North = 0,
    East = 1,
    South = 2,
    West = 3,
    NorthEast,
    SouthEast,
    NorthWest,
    SouthWest,
    None
}