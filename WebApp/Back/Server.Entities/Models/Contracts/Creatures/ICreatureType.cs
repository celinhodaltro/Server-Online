using System.Collections.Generic;
using Game.Common.Creatures;

namespace Server.Entities.Models.Contracts.Creatures;

public interface ICreatureType
{
    string Name { get; }
    uint MaxHealth { get; }
    ushort Speed { get; }
    IDictionary<LookType, ushort> Look { get; }
}