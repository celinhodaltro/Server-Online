using System.Collections.Generic;

namespace Server.Entities.Models.Helpers;

public static class QueueExtensions
{
    public static bool IsEmpty<T>(this Queue<T> queue)
    {
        return queue.Count == 0;
    }
}