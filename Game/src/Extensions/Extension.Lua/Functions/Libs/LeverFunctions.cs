using System;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.World;
using Game.Common.Item;
using Game.Items.Factories;
using Game.World.Models.Tiles;
using Server.Helpers;

namespace Extension.Lua.Functions.Libs;

public static class LeverFunctions
{
    public static void AddLibs(this NLua.Lua lua)
    {
        lua.DoString("lever_lib = {}");
        lua["lever_lib.switch"] = SwitchLever;
    }

    private static void SwitchLever(IItem item)
    {
        var map = IoC.GetInstance<IMap>();

        if (map[item.Location] is not DynamicTile dynamicTile) return;

        var newLeverId = (ushort)(item.Metadata.TypeId == 1946 ? 1945 : 1946);
        var newLever = ItemFactory.Instance.Create(newLeverId, item.Location,
            item.Metadata.Attributes.ToDictionary<ItemAttribute, IConvertible>());

        dynamicTile.RemoveItem(item, 1, out _);
        dynamicTile.AddItem(newLever);
    }
}