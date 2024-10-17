﻿using System;

namespace Server.Entities.Common.Effects;

[AttributeUsage(AttributeTargets.Field)]
public class AreaEffectAttribute : Attribute
{
    public AreaEffectAttribute(string name, bool hasRotation = false)
    {
        Name = name;
        HasRotation = hasRotation;
    }

    public string Name { get; }
    public bool HasRotation { get; }
}