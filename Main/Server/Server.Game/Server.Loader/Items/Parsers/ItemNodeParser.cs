using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Item.Structs;
using Game.Items;
using Loader.OTB.Structure;

namespace Loader.Items.Parsers;

public static class ItemNodeParser
{
    /// <summary>
    ///     Parses ItemNode object to IItemType
    /// </summary>
    /// <param name="itemNode"></param>
    /// <returns></returns>
    public static IItemType Parse(ItemNode itemNode)
    {
        var itemType = new ItemType();

        itemType.SetId(itemNode.ServerId);
        itemType.SetClientId(itemNode.ClientId);
        itemType.SetSpeed(itemNode.Speed);
        itemType.SetLight(new LightBlock(itemNode.LightLevel, itemNode.LightColor));
        itemType.SetGroup((byte)itemNode.Type);

        itemType.ParseOTFlags(itemNode.Flags);

        return itemType;
    }
}