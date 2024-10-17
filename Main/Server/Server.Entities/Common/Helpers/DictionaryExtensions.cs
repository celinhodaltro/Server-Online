﻿using System;
using System.Collections.Generic;

namespace Server.Entities.Common.Helpers;

public static class DictionaryExtensions
{
    public static bool AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, TValue value)
    {
        if (map.TryGetValue(key, out _))
        {
            map[key] = value;
            return true;
        }

        return map.TryAdd(key, value);
    }

    public static bool AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key,
        Func<TValue, TValue> funcValue)
    {
        if (map.TryGetValue(key, out var currentValue))
        {
            map[key] = funcValue.Invoke(currentValue);
            return true;
        }

        return map.TryAdd(key, funcValue.Invoke(currentValue));
    }
}