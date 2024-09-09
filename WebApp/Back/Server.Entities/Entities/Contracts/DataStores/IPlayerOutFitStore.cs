using System.Collections.Generic;
using Server.Entities.Contracts.Creatures;
using Server.Entities.Creatures.Players;

namespace Server.Entities.Contracts.DataStores;

public interface IPlayerOutFitStore : IDataStore<Gender, IEnumerable<IPlayerOutFit>>
{
}