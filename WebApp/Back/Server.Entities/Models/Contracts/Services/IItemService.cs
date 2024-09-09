using Server.Entities.Models.Location.Structs;
using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.Services;

public interface IItemService
{
    IItem Transform(Location.Structs.Location location, ushort fromItemId, ushort toItemId);
    IItem Create(Location.Structs.Location location, ushort id);
}