using System.Collections.Generic;
using Server.Entities.Creatures;

namespace Server.Entities.Contracts.Creatures;

public interface ICreatureType
{
    string Name { get; }
    uint MaxHealth { get; }
    ushort Speed { get; }
    IDictionary<LookType, ushort> Look { get; }
}