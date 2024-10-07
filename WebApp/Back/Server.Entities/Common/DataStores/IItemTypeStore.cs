using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts;

public interface IItemTypeStore : IDataStore<ushort, IItemType>
{
}