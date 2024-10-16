﻿namespace Server.Entities.Common.Contracts.Creatures.Monsters;

public interface IMonsterSummon
{
    byte Chance { get; }
    uint Interval { get; }
    byte Max { get; }
    string Name { get; }
}