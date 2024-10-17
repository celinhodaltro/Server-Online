﻿using Server.Entities.Common.Contracts.Items.Types.Usable;

namespace Server.Entities.Common.Contracts.Items.Types.Runes;

public interface IFieldRune : IUsableOnTile, IRune
{
    string Area { get; }
    ushort Field { get; }
}