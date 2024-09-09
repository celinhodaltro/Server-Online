using System.Collections.Generic;
using Server.Entities.Models.Creatures;

namespace Server.Entities.Models.Contracts.Creatures;

public interface ICreatureType
{
    string Name { get; }
    uint MaxHealth { get; }
    ushort Speed { get; }
    IDictionary<LookType, ushort> Look { get; }
}