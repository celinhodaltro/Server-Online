using System.Collections.Generic;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Results;

namespace Server.Entities.Common.Contracts.World.Tiles;

public interface IOperationResult
{
    List<(IThing, Operation, byte)> Operations { get; }
    bool HasAnyOperation { get; }

    void Add(Operation operation, IThing thing, byte stackPosition = 0);
}