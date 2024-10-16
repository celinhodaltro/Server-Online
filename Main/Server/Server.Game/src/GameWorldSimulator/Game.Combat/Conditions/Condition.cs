﻿using System;
using Server.Entities.Common.Creatures;

namespace Game.Combat.Conditions;

public class Condition : BaseCondition
{
    /// <param name="duration">Duration in milliseconds</param>
    public Condition(ConditionType type, uint duration) : base(duration)
    {
        Type = type;
    }

    public Condition(ConditionType type, uint duration, Action onEndAction) : base(duration, onEndAction)
    {
        Type = type;
    }

    public override ConditionType Type { get; }
}