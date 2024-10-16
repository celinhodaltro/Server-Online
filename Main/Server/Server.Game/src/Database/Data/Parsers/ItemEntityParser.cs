﻿using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Extensions;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Location.Structs;

namespace Data.Parsers;

public static class ItemEntityParser
{
    public static T ToPlayerItemEntity<T>(IItem item) where T : Server.Entities.CharacterItemBase, new()
    {
        var itemModel = new T
        {
            ServerId = (short)item.Metadata.TypeId,
            Amount = item is ICumulative cumulative ? cumulative.Amount : (short)1,
            DecayTo = item.Decay?.DecaysTo,
            DecayDuration = item.Decay?.Duration,
            DecayElapsed = item.Decay?.Elapsed,
            Charges = item is IChargeable chargeable ? chargeable.Charges : null
        };

        return itemModel;
    }

    public static IItem BuildContainer<T>(IContainer container, List<T> items, Location location,
        IItemFactory itemFactory) where T : Server.Entities.CharacterItemBase
    {
        if (items == null || !items.Any())
            return container;

        // Queue to hold the child containers and their corresponding container IDs
        var childrenContainers = new Queue<(IContainer Container, int ContainerId)>();
        childrenContainers.Enqueue((container, 0));

        while (childrenContainers.TryDequeue(out var dequeuedContainer))
        {
            // Get the items that belong to the current container and order them by ID in descending order
            var containerItemsRecords = items.Where(x => x.ParentId == dequeuedContainer.ContainerId)
                .OrderByDescending(x => x.Id).ToList();

            foreach (var itemRecord in containerItemsRecords)
            {
                // Create an item using the item factory, based on the item record
                var item = itemFactory.Create((ushort)itemRecord.ServerId, location, itemRecord.GetAttributes());

                // Add the item to the current container
                dequeuedContainer.Container.AddItem(item);

                // If the item is also a container, set its parent and enqueue it for further processing
                if (item is not IContainer childContainer)
                    continue;

                childContainer.SetParent(dequeuedContainer.Container);
                childrenContainers.Enqueue((childContainer, itemRecord.ContainerId));
            }
        }

        // Return the updated container
        return container;
    }
}