namespace Server.Entities.Contracts.Creatures;

public interface IIntervalChance
{
    byte Chance { get; set; }
    ushort Interval { get; set; }
}