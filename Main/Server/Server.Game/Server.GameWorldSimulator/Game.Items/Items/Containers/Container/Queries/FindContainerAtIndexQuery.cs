﻿using Server.Entities.Common.Contracts.Items.Types.Containers;

namespace Game.Items.Items.Containers.Container.Queries;

internal static class FindContainerAtIndexQuery
{
    public static bool Find(Container container, byte index, out IContainer containerFound)
    {
        containerFound = null;
        if (container.Items.Count <= index || container.Items[index] is not IContainer c) return false;

        containerFound = c;
        return true;
    }
}