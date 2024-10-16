﻿using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Containers;

namespace Game.Items.Items.Containers.Container.Queries;

internal static class FindRootParentQuery
{
    public static IThing Find(IContainer container)
    {
        IThing root = container;
        while (root is IContainer { Parent: not null } parentContainer) root = parentContainer.Parent;
        return root;
    }
}