using System.Collections.Generic;
using Game.Common.Contracts.Items;
using Game.Common.Results;

namespace Game.Common.Contracts.World.Tiles;

public interface IOperationResult
{
    List<(IThing, Operation, byte)> Operations { get; }
    bool HasAnyOperation { get; }

    void Add(Operation operation, IThing thing, byte stackPosition = 0);
}