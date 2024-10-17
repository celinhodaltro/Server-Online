﻿using System.Linq;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Results;

namespace Extension.Lua.Functions;

public static class PlayerFunctions
{
    public static void AddPlayerFunctions(this NLua.Lua lua)
    {
        lua.DoString("player_helper = {}");
        lua["player_helper.addToBackpack"] = AddToBackpackFunction;
    }

    private static LuaResult AddToBackpackFunction(IPlayer player, params IItem[] items)
    {
        var result = AddToBackpack(player, items);
        return new LuaResult(result.Succeeded, result.Error);
    }

    private static Result<IItem> AddToBackpack(IPlayer player, params IItem[] items)
    {
        if (Guard.AnyNull(player, items)) return Result<IItem>.NotPossible;

        if (!items.Any()) return Result<IItem>.Fail(InvalidOperation.NotPossible);

        if (player.Inventory.BackpackSlot is not { } backpack)
            return Result<IItem>.Fail(InvalidOperation.NotEnoughRoom);

        float totalWeight = 0;

        foreach (var item in items)
        {
            if (item is null) continue;
            if (!item.IsPickupable) continue;
            totalWeight += item.Weight;
        }

        if (player.TotalCapacity < totalWeight) return Result<IItem>.Fail(InvalidOperation.TooHeavy);

        if (backpack.TotalOfFreeSlots < items.Length)
            return Result<IItem>.Fail(InvalidOperation.NotEnoughRoom);

        foreach (var item in items) player.Inventory.AddItem(item, Slot.Backpack);

        return Result<IItem>.Success;
    }
}