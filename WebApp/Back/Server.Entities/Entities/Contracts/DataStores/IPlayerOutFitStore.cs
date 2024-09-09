using System.Collections.Generic;
using Game.Common.Contracts.Creatures;
using Game.Common.Creatures.Players;

namespace Game.Common.Contracts.DataStores;

public interface IPlayerOutFitStore : IDataStore<Gender, IEnumerable<IPlayerOutFit>>
{
}