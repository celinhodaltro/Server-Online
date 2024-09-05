using System.Collections.Generic;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;
using Game.Common.Creatures.Players;

namespace Data.InMemory;

public class PlayerOutFitStore : DataStore<PlayerOutFitStore, Gender, IEnumerable<IPlayerOutFit>>, IPlayerOutFitStore
{
}