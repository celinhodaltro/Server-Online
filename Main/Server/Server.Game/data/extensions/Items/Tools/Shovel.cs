using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Services;
using Server.Entities.Common.Texts;
using Game.Items.Items.UsableItems;
using Game.World.Map;
using Server.Helpers;

namespace Extensions.Items.Tools;

public class Shovel : UsableOnItem, IUsableOnItem
{
    public Shovel(IItemType type, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(
        type, location)
    {
    }

    public bool Use(ICreature usedBy, IItem onItem)
    {
        if (!CanUse(usedBy, onItem))
        {
            OperationFailService.Send(usedBy.CreatureId, TextConstants.NOT_POSSIBLE);
            return false;
        }

        var result = OpenCaveHole(usedBy, onItem);
        if (!result) OperationFailService.Send(usedBy.CreatureId, TextConstants.NOT_POSSIBLE);

        return result;
    }

    public new static bool IsApplicable(IItemType type)
    {
        return UsableOnItem.IsApplicable(type) &&
               (type.OnUse?.HasAttribute(ItemAttribute.TransformTo) ?? false) &&
               type.ClientId == 2554;
    }

    private bool OpenCaveHole(ICreature usedBy, IItem item)
    {
        if (Map.Instance[item.Location] is not IDynamicTile tile) return false;
        if (tile.Ground is null) return false;

        var useOnItems = Metadata.OnUse.GetAttributeArray<ushort>(ItemAttribute.UseOn);
        if (useOnItems == default) return false;

        if (!useOnItems.Contains(tile.Ground.ServerId)) return false;
        if (usedBy is not IPlayer player) return false;

        var transformService = IoC.GetInstance<IItemTransformService>();
        transformService.Transform(player, tile.Ground, tile.Ground.Metadata.TransformTo);

        tile.Ground.Decay?.StartDecay();

        Map.Instance.TryMoveCreature(usedBy, tile.Location);

        return true;
    }
}