﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Results;
using Game.Items.Bases;

namespace Game.Items.Items;

public class Paper : BaseItem, IReadable
{
    public Paper(IItemType metadata, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(
        metadata, location)
    {
        attributes.TryGetValue(ItemAttribute.Text, out var text);
        Text = text?.ToString(CultureInfo.InvariantCulture);
    }

    public string Text { get; private set; }
    public ushort MaxLength => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.MaxLength);
    public bool CanWrite => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Writeable) == 1;

    public string
        WrittenBy { get; private set; } //todo: change to id and then query the database to get the current name

    public DateTime? WrittenOn { get; set; }

    public Result Write(string text, IPlayer writtenBy)
    {
        if (!CanWrite) return Result.Fail(InvalidOperation.NotPossible);

        if (text.IsNull()) return Result.Success;

        if (text.Length > MaxLength) return Result.Fail(InvalidOperation.NotPossible);

        Text = text;
        WrittenBy = writtenBy.Name;
        WrittenOn = DateTime.Now;
        return Result.Success;
    }

    public void Use(IPlayer usedBy)
    {
        usedBy.Read(this);
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.Paper;
    }
}