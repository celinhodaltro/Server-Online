using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Bases;

namespace Game.Items.Items;

public class TeleportItem : BaseItem, ITeleport
{
    public TeleportItem(IItemType metadata, Location location,
        IDictionary<ItemAttribute, IConvertible> attributes) : base(metadata, location)
    {
        Destination = Location.Zero;

        if (attributes is not null)
            Destination = attributes.TryGetValue(ItemAttribute.TeleportDestination, out var destination) &&
                          destination is Location destLocation
                ? destLocation
                : Location.Zero;
    }

    private Location Destination { get; }

    public bool HasDestination => Destination != Location.Zero;

    public bool Teleport(IWalkableCreature player)
    {
        if (!HasDestination) return false;
        player.TeleportTo(Destination);
        return true;
    }

    public static bool IsApplicable(IItemType type)
    {
        return type
            .Attributes
            .GetAttribute(ItemAttribute.Type)
            ?.Equals("teleport", StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
}