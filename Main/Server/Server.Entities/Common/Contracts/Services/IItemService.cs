using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts.Services;

public interface IItemService
{
    IItem Transform(Location.Structs.Location location, ushort fromItemId, ushort toItemId);
    IItem Create(Location.Structs.Location location, ushort id);
}