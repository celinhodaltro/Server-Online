using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Characters;

namespace Server.Entities.Common.Contracts;

public interface IPlayerOutFitStore : IDataStore<Gender, IEnumerable<IPlayerOutFit>>
{
}