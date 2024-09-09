using System.Collections.Generic;

namespace Game.Common.Contracts.Creatures;

public interface ILoot
{
    ILootItem[] Items { get; }
    HashSet<ICreature> Owners { get; }
    ILootItem[] Drop();
}