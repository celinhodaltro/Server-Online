﻿using Server.Entities.Common.Contracts.Items.Types.Body;
using Server.Entities.Common.Creatures.Players;

namespace Game.Creatures.Player.Inventory.Calculations;

internal static class InventoryDefenseCalculation
{
    internal static ushort CalculateTotalDefense(this InventoryMap inventory)
    {
        var totalDefense = 0;
        totalDefense += inventory.GetItem<IWeaponItem>(Slot.Left)?.Defense ?? 0;

        totalDefense += inventory.GetItem<IDefenseEquipment>(Slot.Right)?.DefenseValue ?? 0;

        return (ushort)totalDefense;
    }

    internal static ushort CalculateTotalArmor(this InventoryMap inventoryMap)
    {
        ushort totalArmor = 0;

        byte GetDefenseValue(Slot slot)
        {
            return (byte)(inventoryMap.GetItem<IDefenseEquipment>(slot)?.DefenseValue ?? default);
        }

        totalArmor += GetDefenseValue(Slot.Necklace);
        totalArmor += GetDefenseValue(Slot.Head);
        totalArmor += GetDefenseValue(Slot.Body);
        totalArmor += GetDefenseValue(Slot.Legs);
        totalArmor += GetDefenseValue(Slot.Feet);
        totalArmor += GetDefenseValue(Slot.Ring);

        return totalArmor;
    }
}