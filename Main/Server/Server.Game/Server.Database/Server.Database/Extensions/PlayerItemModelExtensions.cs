﻿using System;
using System.Collections.Generic;
using Data.Entities;
using Server.Entities.Common.Item;

namespace Data.Extensions;

public static class PlayerItemModelExtensions
{
    public static Dictionary<ItemAttribute, IConvertible> GetAttributes(this PlayerItemBaseEntity itemEntity)
    {
        var attributes = new Dictionary<ItemAttribute, IConvertible>
        {
            { ItemAttribute.Count, itemEntity.Amount }
        };

        if (itemEntity.Charges > 0) attributes.Add(ItemAttribute.Charges, itemEntity.Charges);

        if (itemEntity.DecayDuration > 0)
        {
            attributes.Add(ItemAttribute.DecayTo, itemEntity.DecayTo);
            attributes.Add(ItemAttribute.DecayElapsed, itemEntity.DecayElapsed);
            attributes.Add(ItemAttribute.Duration, itemEntity.DecayDuration);
        }

        return attributes;
    }
}