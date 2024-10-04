using System.Collections.Generic;
using Server.Entities.Common.Characters;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;


namespace Data.InMemory;

public class PlayerOutFitStore : DataStore<PlayerOutFitStore, Gender, IEnumerable<IPlayerOutFit>>, IPlayerOutFitStore
{
}