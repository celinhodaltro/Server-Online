using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.Contracts.Services;

public interface IItemTransformService
{
    Result<IItem> Transform(IPlayer by, IItem fromItem, ushort toItem);
    Result<IItem> Transform(IItem fromItem, ushort toItem);
}