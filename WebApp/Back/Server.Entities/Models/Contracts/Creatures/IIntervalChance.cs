namespace Server.Entities.Models.Contracts.Creatures;

public interface IIntervalChance
{
    byte Chance { get; set; }
    ushort Interval { get; set; }
}