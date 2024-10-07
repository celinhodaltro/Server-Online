using System.Collections.Generic;
using Server.Entities.Common.Creatures;

namespace Server.Entities.Common.Contracts.Creatures;

public interface ICreatureType
{
    string Name { get; }
    uint MaxHealth { get; }
    ushort Speed { get; }
    IDictionary<LookType, ushort> Look { get; }
}