using System;
using System.Collections.Generic;
using System.Reflection;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Location;

namespace Data.InMemory;

public class AreaEffectStore : IAreaEffectStore
{
    private static readonly Dictionary<string, FieldInfo> Areas = new(StringComparer.InvariantCultureIgnoreCase);

    private static readonly Dictionary<string, byte[][,]> Waves = new(StringComparer.InvariantCultureIgnoreCase);

    public void Add(string name, FieldInfo area)
    {
        Areas.TryAdd(name, area);
    }

    public void Add(string name, FieldInfo area, byte[][,] areas)
    {
        Areas.TryAdd(name, area);
        Waves.TryAdd(name, areas);
    }

    public byte[,] Get(string name)
    {
        return Areas.TryGetValue(name, out var area) ? (byte[,])area.GetValue(null) : default;
    }

    public byte[,] Get(string name, Direction direction)
    {
        if (!Waves.TryGetValue(name, out var areas)) return default;

        return areas[(byte)direction];
    }
}