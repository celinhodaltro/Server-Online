﻿using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Inspection;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location;
using Server.Entities.Common.Location.Structs;

namespace Game.World.Models.Tiles;

public abstract class BaseTile : ITile
{
    protected uint Flags;

    public bool CannotLogout => HasFlag(TileFlags.NoLogout);
    public FloorChangeDirection FloorDirection { get; protected set; } = FloorChangeDirection.None;
    public bool ProtectionZone => HasFlag(TileFlags.ProtectionZone);
    public abstract IItem TopItemOnStack { get; }
    public abstract ICreature TopCreatureOnStack { get; }
    public abstract int ThingsCount { get; }
    public bool HasThings => ThingsCount > 0;
    public abstract bool TryGetStackPositionOfThing(IPlayer player, IThing thing, out byte stackPosition);

    public abstract byte GetCreatureStackPositionIndex(IPlayer observer);

    public bool HasFlag(TileFlags flag)
    {
        return ((uint)flag & Flags) != 0;
    }

    public bool BlockMissile => HasFlag(TileFlags.BlockMissile);

    public Location Location { get; private set; }

    public void SetNewLocation(Location location)
    {
        if (Location != default) throw new InvalidOperationException();
        Location = location;
    }

    public string Name { get; }

    public string GetLookText(IInspectionTextBuilder inspectionTextBuilder, IPlayer player, bool isClose = false)
    {
        return string.Empty;
    }

    public void Use(IPlayer usedBy)
    {
    }

    protected void SetFlag(TileFlags flag)
    {
        Flags |= (uint)flag;
    }

    protected void RemoveFlag(TileFlags flag)
    {
        Flags &= ~(uint)flag;
    }

    protected void SetTileFlags(IItem item)
    {
        if (item is null) return;

        if (FloorDirection == FloorChangeDirection.None && !item.IsUsable) FloorDirection = item.FloorDirection;

        if (item.Metadata.HasFlag(ItemFlag.Unpassable) && !item.CanBeMoved) SetFlag(TileFlags.ImmovableBlockSolid);

        if (item.Metadata.HasFlag(ItemFlag.BlockPathFind)) SetFlag(TileFlags.BlockPath);

        if (item.Metadata.HasFlag(ItemFlag.HasHeight)) SetFlag(TileFlags.HasHeight);

        if (item.Metadata.HasFlag(ItemFlag.Unpassable) && item is not IMagicField)
        {
            SetFlag(TileFlags.NoFieldBlockPath);

            if (!item.CanBeMoved) SetFlag(TileFlags.ImmovableNoFieldBlockPath);
        }

        if (item.Metadata.HasFlag(ItemFlag.BlockProjectTile)) SetFlag(TileFlags.BlockMissile);

        if (item.Metadata.Attributes.TryGetAttribute(ItemAttribute.BlockProjectTile, out int value) && value == 1)
            SetFlag(TileFlags.BlockMissile);

        if (item is ITeleport) SetFlag(TileFlags.Teleport);

        if (item is IMagicField) SetFlag(TileFlags.MagicField);

        // if (item->getMailbox()) { //todo
        //     setFlag(TILESTATE_MAILBOX);
        // }

        // if (item->getTrashHolder()) { //todo
        //     setFlag(TILESTATE_TRASHHOLDER);
        // }

        if (item.Metadata.HasFlag(ItemFlag.Unpassable)) SetFlag(TileFlags.Unpassable);

        // if (item->getBed()) { //todo
        //     setFlag(TILESTATE_BED);
        // }


        if (item is IDepot) SetFlag(TileFlags.Depot);

        if (item.Metadata.HasFlag(ItemFlag.Hangable)) //todo: might be wrong
            SetFlag(TileFlags.SupportsHangable);
    }

    protected void ResetTileFlags(params IItem[] items)
    {
        FloorDirection = FloorChangeDirection.None;
        RemoveFlag(TileFlags.ImmovableBlockSolid);
        RemoveFlag(TileFlags.BlockPath);
        RemoveFlag(TileFlags.ImmovableNoFieldBlockPath);
        RemoveFlag(TileFlags.BlockMissile);
        RemoveFlag(TileFlags.Teleport);
        RemoveFlag(TileFlags.MagicField);
        RemoveFlag(TileFlags.Unpassable);
        RemoveFlag(TileFlags.Depot);
        RemoveFlag(TileFlags.SupportsHangable);
        RemoveFlag(TileFlags.MailBox);
        RemoveFlag(TileFlags.TrashHolder);
        RemoveFlag(TileFlags.Bed);

        foreach (var item in items) SetTileFlags(item);
    }
}