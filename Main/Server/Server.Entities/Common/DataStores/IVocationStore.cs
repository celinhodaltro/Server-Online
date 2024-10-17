using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts;

public interface IVocationStore : IDataStore<byte, IVocation>, IDataStore
{
}