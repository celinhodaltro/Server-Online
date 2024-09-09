using System.Collections.Generic;
using Game.Common.Creatures;

namespace Game.Common.Contracts.Creatures;

public interface ICreatureType
{
    string Name { get; }
    uint MaxHealth { get; }
    ushort Speed { get; }
    IDictionary<LookType, ushort> Look { get; }
}