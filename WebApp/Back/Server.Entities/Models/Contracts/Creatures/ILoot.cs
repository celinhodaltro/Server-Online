using System.Collections.Generic;

namespace Server.Entities.Models.Contracts.Creatures;

public interface ILoot
{
    ILootItem[] Items { get; }
    HashSet<ICreature> Owners { get; }
    ILootItem[] Drop();
}