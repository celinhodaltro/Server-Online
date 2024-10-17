﻿namespace Server.Entities.Common.Location;

public enum FloorChangeDirection : byte
{
    None = default,
    Up,
    Down,
    South,
    SouthAlternative,
    EastAlternative,
    North,
    East,
    West
}