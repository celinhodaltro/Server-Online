﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Bases;
using Game.Items.Mappers;

namespace Game.Items.Items;

public class LiquidPool : BaseItem, ILiquid
{
    public LiquidPool(IItemType type, Location location, LiquidColor color) : base(type, location)
    {
        LiquidColor = LiquidColor.Empty;
        LiquidColor = GetLiquidColor(color);
    }

    public LiquidPool(IItemType type, Location location,
        IDictionary<ItemAttribute, IConvertible> attributes) : base(type, location)
    {
        LiquidColor = LiquidColor.Empty;
        LiquidColor = GetLiquidColor(attributes);
    }

    public bool IsLiquidPool => Metadata.Group == ItemGroup.Splash;
    public bool IsLiquidSource => Metadata.Flags.Contains(ItemFlag.LiquidSource);
    public bool IsLiquidContainer => Metadata.Group == ItemGroup.Fluid;
    public LiquidColor LiquidColor { get; }
    public ushort ClientId => Metadata.ClientId;

    public Span<byte> GetRaw()
    {
        Span<byte> cache = stackalloc byte[3];
        var idBytes = BitConverter.GetBytes(ClientId);

        cache[0] = idBytes[0];
        cache[1] = idBytes[1];
        cache[2] = (byte)LiquidColor;

        return cache.ToArray();
    }

    private LiquidColor GetLiquidColor(LiquidColor color)
    {
        if (!IsLiquidPool && !IsLiquidContainer) return 0x00;

        return color;
    }

    private LiquidColor GetLiquidColor(IDictionary<ItemAttribute, IConvertible> attributes)
    {
        if (!IsLiquidPool && !IsLiquidContainer) return 0x00;
        if (attributes != null && attributes.TryGetValue(ItemAttribute.Count, out var count))
            return new LiquidTypeMap()[(byte)count];
        return LiquidColor.Empty;
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group == ItemGroup.Splash ||
               type.Flags.Contains(ItemFlag.LiquidSource) ||
               type.Group == ItemGroup.Fluid;
    }
}