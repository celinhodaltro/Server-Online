using System.Collections.Generic;
using System.Linq;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.World.Tiles;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.World;

public struct OperationResult : IOperationResult
{
    public List<(IThing, Operation, byte)> Operations { get; private set; }

    public void Add(Operation operation, IThing thing, byte stackPosition = 0)
    {
        Operations = Operations ?? new List<(IThing, Operation, byte)>();
        Operations.Add((thing, operation, stackPosition));
    }

    public OperationResult(Operation operation, IThing thing, byte stackPosition = 0)
    {
        Operations = new List<(IThing, Operation, byte)>();
        Operations.Add((thing, operation, stackPosition));
    }

    public bool HasAnyOperation => Operations?.Any() ?? false;
}