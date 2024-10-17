using System.Collections.Generic;

namespace Server.Entities.Common.Contracts.Creatures;

public interface ILoot
{
    ILootItem[] Items { get; }
    HashSet<ICreature> Owners { get; }
    ILootItem[] Drop();
}