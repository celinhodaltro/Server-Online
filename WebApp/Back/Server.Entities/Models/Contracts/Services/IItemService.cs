using Game.Common.Location.Structs;
using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.Services;

public interface IItemService
{
    IItem Transform(Location location, ushort fromItemId, ushort toItemId);
    IItem Create(Location location, ushort id);
}