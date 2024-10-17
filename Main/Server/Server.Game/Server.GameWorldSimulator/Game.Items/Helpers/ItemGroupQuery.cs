﻿using System;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;

namespace Game.Items.Helpers;

internal static class ItemGroupQuery
{
    public static ItemGroup Find(ItemType metadata)
    {
        if (Guard.IsNull(metadata)) return ItemGroup.None;

        var queries = new[]
        {
            GetWeaponGroup,
            GetDefenseEquipmentGroup,
            GetFloorChangeGroup,
            GetMagicFieldGroup,
            GetPaperGroup,
            GetSignGroup,
            GetTeleportGroup,
            GetContainerGroup,
            GetCumulativeGroup,
            GetUsableOnItemGroup
        };

        return GetItemGroup(metadata, queries);
    }

    private static ItemGroup GetItemGroup(IItemType metadata, Func<IItemType, ItemGroup>[] queries)
    {
        foreach (var query in queries)
        {
            var itemGroup = query.Invoke(metadata);
            if (itemGroup is not ItemGroup.None) return itemGroup;
        }

        return ItemGroup.None;
    }

    private static ItemGroup GetWeaponGroup(IItemType metadata)
    {
        return metadata.WeaponType switch
        {
            WeaponType.Axe or WeaponType.Club or WeaponType.Sword => ItemGroup.MeleeWeapon,
            WeaponType.Magical => ItemGroup.MagicWeapon,
            WeaponType.Distance => metadata.HasFlag(ItemFlag.Stackable)
                ? ItemGroup.ThrowableDistanceWeapon
                : ItemGroup.DistanceWeapon,
            WeaponType.Ammunition => ItemGroup.Ammo,
            _ => ItemGroup.None
        };
    }

    private static ItemGroup GetDefenseEquipmentGroup(IItemType metadata)
    {
        return metadata.BodyPosition switch
        {
            Slot.Body => true,
            Slot.Legs => true,
            Slot.Head => true,
            Slot.Feet => true,
            Slot.Right => true,
            Slot.Ring => true,
            Slot.Necklace => true,
            _ => false
        } || metadata.WeaponType == WeaponType.Shield
            ? ItemGroup.BodyDefenseEquipment
            : ItemGroup.None;
    }

    private static ItemGroup GetFloorChangeGroup(IItemType metadata)
    {
        return metadata.Attributes.HasAttribute(ItemAttribute.FloorChange) && metadata.HasFlag(ItemFlag.Usable)
            ? ItemGroup.FloorChanger
            : ItemGroup.None;
    }

    private static ItemGroup GetMagicFieldGroup(IItemType metadata)
    {
        return metadata.Attributes.GetAttribute(ItemAttribute.Type)
            ?.Equals("magicfield", StringComparison.InvariantCultureIgnoreCase) ?? false
            ? ItemGroup.MagicField
            : ItemGroup.None;
    }

    private static ItemGroup GetPaperGroup(IItemType metadata)
    {
        return metadata.Flags.Contains(ItemFlag.Readable) ? ItemGroup.Paper : ItemGroup.None;
    }

    private static ItemGroup GetSignGroup(IItemType metadata)
    {
        return (metadata.Attributes.HasAttribute(ItemAttribute.Text) && !metadata.Flags.Contains(ItemFlag.Usable)) ||
               (metadata.Attributes.GetAttribute(ItemAttribute.Type)
                   ?.Equals("sign", StringComparison.InvariantCultureIgnoreCase) ?? false)
            ? ItemGroup.Sign
            : ItemGroup.None;
    }

    private static ItemGroup GetTeleportGroup(IItemType metadata)
    {
        return metadata.Attributes.GetAttribute(ItemAttribute.Type)
            ?.Equals("teleport", StringComparison.InvariantCultureIgnoreCase) ?? false
            ? ItemGroup.Teleport
            : ItemGroup.None;
    }

    private static ItemGroup GetContainerGroup(IItemType metadata)
    {
        var type = metadata.Attributes.GetAttribute(ItemAttribute.Type);

        if (type is null) return ItemGroup.None;

        if (type.Equals("container", StringComparison.InvariantCultureIgnoreCase)) return ItemGroup.Container;

        return ItemGroup.None;
    }

    private static ItemGroup GetCumulativeGroup(IItemType metadata)
    {
        if (!ICumulative.IsApplicable(metadata)) return ItemGroup.None;

        if (metadata.Attributes?.HasAttribute(ItemAttribute.Healing) ?? false)
            return ItemGroup.Healing;

        var type = metadata.Attributes?.GetAttribute(ItemAttribute.Type);

        if (type is null) return ItemGroup.None;

        if (type.Equals("coin", StringComparison.InvariantCultureIgnoreCase)) return ItemGroup.Coin;

        if (type.Equals("food", StringComparison.InvariantCultureIgnoreCase)) return ItemGroup.Food;

        var itemGroup = GetRuneGroup(metadata);

        return itemGroup != ItemGroup.None ? itemGroup : ItemGroup.Cumulative;
    }

    private static ItemGroup GetRuneGroup(IItemType metadata)
    {
        if (!ICumulative.IsApplicable(metadata)) return ItemGroup.None;

        var type = metadata.Attributes.GetAttribute(ItemAttribute.Type);

        if (type is null) return ItemGroup.None;

        if (!type.Equals("rune", StringComparison.InvariantCultureIgnoreCase)) return ItemGroup.None;

        if (metadata.Attributes.HasAttribute(ItemAttribute.Damage)) return ItemGroup.AttackRune;
        if (metadata.Attributes.HasAttribute(ItemAttribute.Field)) return ItemGroup.FieldRune;

        return ItemGroup.None;
    }

    private static ItemGroup GetUsableOnItemGroup(IItemType metadata)
    {
        if (!metadata.Flags.Contains(ItemFlag.Usable) || !metadata.Flags.Contains(ItemFlag.Pickupable))
            return ItemGroup.None;

        if (metadata.OnUse?.HasAttribute(ItemAttribute.FloorChange) ?? false) return ItemGroup.UsableFloorChanger;

        return ItemGroup.UsableOn;
    }
}