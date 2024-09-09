using System.Collections.Generic;
using Game.Common.Creatures.Players;
using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.DataStores;

public interface IPlayerOutFitStore : IDataStore<Gender, IEnumerable<IPlayerOutFit>>
{
}