using Server.Entities.Common.Contracts.Creatures;

namespace Game.Creatures.Monster.Combat;

public struct IntervalChance : IIntervalChance
{
    public IntervalChance(ushort interval, byte chance)
    {
        Interval = interval;
        Chance = chance;
    }

    public ushort Interval { get; set; }
    public byte Chance { get; set; }
}