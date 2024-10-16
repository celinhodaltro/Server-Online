using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts;

public interface ICoinTypeStore : IDataStore<ushort, IItemType>
{
}