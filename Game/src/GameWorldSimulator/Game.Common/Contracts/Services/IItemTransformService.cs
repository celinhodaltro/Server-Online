using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Results;

namespace Game.Common.Contracts.Services;

public interface IItemTransformService
{
    Result<IItem> Transform(IPlayer by, IItem fromItem, ushort toItem);
    Result<IItem> Transform(IItem fromItem, ushort toItem);
}