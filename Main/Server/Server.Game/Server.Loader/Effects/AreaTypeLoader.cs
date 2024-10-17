﻿using System.Linq;
using System.Reflection;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Effects;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location;
using Loader.Interfaces;

namespace Loader.Effects;

public class AreaTypeLoader : IStartupLoader
{
    private readonly IAreaEffectStore _areaEffectStore;

    public AreaTypeLoader(IAreaEffectStore areaEffectStore)
    {
        _areaEffectStore = areaEffectStore;
    }

    public void Load()
    {
        var fields = GameAssemblyCache.Cache.SelectMany(x => x.GetFields())
            .Where(prop => prop.IsDefined(typeof(AreaEffectAttribute), false));

        foreach (var field in fields)
        {
            var attr = field.GetCustomAttribute<AreaEffectAttribute>();
            if (attr is null) continue;

            if (attr.HasRotation)
            {
                var areas = Rotate(attr.Name, (byte[,])field.GetValue(null));
                _areaEffectStore.Add(attr.Name, field, areas);
                continue;
            }

            _areaEffectStore.Add(attr.Name, field);
        }
    }

    private static byte[][,] Rotate(string name, byte[,] area)
    {
        var areas = new byte[4][,];

        areas[(byte)Direction.West] = area;
        areas[(byte)Direction.North] = area.Rotate();
        areas[(byte)Direction.East] = areas[(byte)Direction.North].Rotate();
        areas[(byte)Direction.South] = areas[(byte)Direction.East].Rotate();

        return areas;
    }
}