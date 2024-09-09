using System.Collections.Generic;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.Contracts.World.Tiles;

public interface IOperationResult
{
    List<(IThing, Operation, byte)> Operations { get; }
    bool HasAnyOperation { get; }

    void Add(Operation operation, IThing thing, byte stackPosition = 0);
}