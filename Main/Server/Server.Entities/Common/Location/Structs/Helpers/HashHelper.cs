﻿namespace Server.Entities.Common.Location.Structs.Helpers;

public static class HashHelper
{
    public const int START = 1610612741;

    /// <summary>
    ///     Combines the current hashcode with the hashcode of another object.
    /// </summary>
    public static int CombineHashCode<T>(this int hashCode, T arg)
    {
        unchecked
        {
            return 16777619 * hashCode + arg.GetHashCode();
        }
    }
}