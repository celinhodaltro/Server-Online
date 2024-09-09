using System.Collections.Generic;

namespace Server.Entities.Contracts.Creatures;

public interface ILoot
{
    ILootItem[] Items { get; }
    HashSet<ICreature> Owners { get; }
    ILootItem[] Drop();
}