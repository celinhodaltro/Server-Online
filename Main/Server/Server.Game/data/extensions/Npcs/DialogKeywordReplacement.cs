﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;

namespace Extensions.Npcs;

public class DialogKeywordReplacement
{
    public IEnumerable<Func<IPlayer, string, string>> ReplaceFunctions
    {
        get { yield return (p, m) => m.Replace("|PLAYERNAME|", p.Name); }
    }
}