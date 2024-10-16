﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Location.Structs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Game.World.Models.Tiles;

public class StaticTile : BaseTile, IStaticTile
{
    private IItem _topItemOnStack;

    public StaticTile(Coordinate coordinate, params IItem[] items)
    {
        SetNewLocation(new Location((ushort)coordinate.X, (ushort)coordinate.Y, (byte)coordinate.Z));
        Raw = GetRaw(items);
        ThingsCount = items.Length;
    }

    public override int ThingsCount { get; }
    public byte[] Raw { get; }
    public override IItem TopItemOnStack => _topItemOnStack;
    public override ICreature TopCreatureOnStack => null;

    public override bool TryGetStackPositionOfThing(IPlayer player, IThing thing, out byte stackPosition)
    {
        stackPosition = default;
        return false;
    }

    public override byte GetCreatureStackPositionIndex(IPlayer observer)
    {
        return 0;
    }

    public ushort[] AllClientIdItems
    {
        get
        {
            var itemsId = new ushort[Raw.Length / 2];
            var index = 0;

            for (var i = 0; i < Raw.Length; i += 2)
            {
                var final = i + 2;
                itemsId[index++] = BitConverter.ToUInt16(Raw[i..final]);
            }

            return itemsId;
        }
    }

    public byte[] GetRaw(IItem[] items)
    {
        var ground = new List<byte>();
        var top1 = new List<byte>();
        var downRawItems = new List<byte>();

        foreach (var item in items)
        {
            if (item is null) continue;

            if (item is IGround groundItem)
            {
                _topItemOnStack = groundItem;
                ground.AddRange(BitConverter.GetBytes(item.ClientId));
                continue;
            }

            if (item.IsAlwaysOnTop)
            {
                if (item.FloorDirection != default) FloorDirection = item.FloorDirection;

                _topItemOnStack = item;
                top1.AddRange(BitConverter.GetBytes(item.ClientId));
            }
            else
            {
                _topItemOnStack = item;
                downRawItems.InsertRange(0, BitConverter.GetBytes(item.ClientId));
            }

            SetTileFlags(item);
        }

        return ground.Concat(top1).Concat(downRawItems).ToArray();
    }

    public override int GetHashCode()
    {
        return HashHelper.START
            .CombineHashCode(Raw);
    }
}