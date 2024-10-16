using System.Collections.Generic;
using Server.Entities.Common.Contracts.Items;

namespace Game.Items.Helpers;

public static class OverridenFunctionQuery
{
    public static List<T> Find<T>(IItem item, Dictionary<string, T> useFunctionMap) where T : class
    {
        var functions = new List<T>(3);

        if (useFunctionMap.TryGetValue($"id:{item.Metadata.TypeId}", out var useFunc)) functions.Add(useFunc);

        if (item.ActionId != 0 && (useFunctionMap.TryGetValue($"aid:{item.ActionId}", out useFunc) ||
                                   useFunctionMap.TryGetValue($"id:{item.Metadata.TypeId}-aid:{item.ActionId}",
                                       out useFunc)))
            functions.Add(useFunc);

        if (item.UniqueId != 0 && useFunctionMap.TryGetValue($"uid:{item.UniqueId}", out useFunc))
            functions.Add(useFunc);

        return functions;
    }
}