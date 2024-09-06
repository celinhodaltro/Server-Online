using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items.Types.Containers;

namespace Data.Interfaces;

public interface IPlayerDepotItemRepository : IBaseRepositoryNeo<PlayerDepotItemEntity>
{
    Task<IEnumerable<PlayerDepotItemEntity>> GetByPlayerId(uint id);
    Task Save(IPlayer player, IDepot depot);
}