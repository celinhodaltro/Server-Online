﻿using System.Collections.Generic;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Containers;

namespace Game.Items.Items.Containers.Container.Queries;

public static class FindFirstItemByClientIdQuery
{
    public static (IItem ItemFound, IContainer Container, byte SlotIndex) Find(IContainer onContainer, ushort clientId)
    {
        var containers = new Queue<IContainer>();
        containers.Enqueue(onContainer);

        while (containers.TryDequeue(out var container))
        {
            byte slotIndex = 0;
            foreach (var containerItem in container.Items)
            {
                AddInnerContainerToQueue(containerItem, containers);
                if (containerItem.ClientId == clientId) return (containerItem, container, slotIndex);

                slotIndex++;
            }
        }

        return (null, null, 0);
    }

    private static void AddInnerContainerToQueue(IItem containerItem, Queue<IContainer> containers)
    {
        if (containerItem is not IContainer innerContainer) return;
        containers.Enqueue(innerContainer);
    }
}