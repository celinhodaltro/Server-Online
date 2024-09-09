using System.Collections.Generic;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Creatures.Players;

namespace Server.Entities.Models.Contracts.DataStores;

public interface IPlayerOutFitStore : IDataStore<Gender, IEnumerable<IPlayerOutFit>>
{
}