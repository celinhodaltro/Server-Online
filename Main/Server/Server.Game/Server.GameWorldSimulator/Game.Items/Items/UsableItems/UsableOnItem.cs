using System.Collections;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Bases;

namespace Game.Items.Items.UsableItems;

public class UsableOnItem : BaseItem, IUsableOnItem
{
    public UsableOnItem(IItemType type, Location location) : base(type, location)
    {
    }

    public virtual bool AllowUseOnDistance => false;

    public virtual bool CanUseOn(IItem onItem)
    {
        var useOnItems = Metadata.OnUse?.GetAttributeArray<ushort>(ItemAttribute.UseOn);

        return useOnItems is not null && useOnItems.Contains(onItem.Metadata.TypeId);
    }

    public bool CanUseOn(ushort[] items, IItem onItem)
    {
        return ((IList)items)?.Contains(onItem.Metadata.TypeId) ?? false;
    }

    public virtual bool CanUse(ICreature usedBy, IItem onItem)
    {
        if (!AllowUseOnDistance && !usedBy.Location.IsNextTo(onItem.Location)) return false;
        return usedBy.Location.SameFloorAs(onItem.Location);
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.UsableOn;
    }
}