using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Results;

namespace Server.Entities.Common.Contracts.Services;

public interface IItemTransformService
{
    Result<IItem> Transform(IPlayer by, IItem fromItem, ushort toItem);
    Result<IItem> Transform(IItem fromItem, ushort toItem);
}