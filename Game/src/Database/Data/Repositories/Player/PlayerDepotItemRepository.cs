using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items.Types.Containers;
using Serilog;

namespace Data.Repositories.Player;

/// <summary>
///     Repository class for managing PlayerDepotItem entity.
/// </summary>
public class PlayerDepotItemRepository : BaseRepository<PlayerDepotItemEntity>,
    IPlayerDepotItemRepository
{
    #region constructors

    public PlayerDepotItemRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    #endregion

    #region public methods implementation

    public async Task<IEnumerable<PlayerDepotItemEntity>> GetByPlayerId(uint id)
    {
        await using var context = NewDbContext;
        return await context.PlayerDepotItems
            .Where(c => c.PlayerId == id)
            .ToListAsync();
    }

    private static async Task DeleteAll(uint playerId, NeoContext neoContext)
    {
        var items = await neoContext.PlayerDepotItems.Where(x => x.PlayerId == playerId).ToListAsync();
        neoContext.PlayerDepotItems.RemoveRange(items);
    }

    public async Task Save(IPlayer player, IDepot depot)
    {
        await using var context = NewDbContext;

        await DeleteAll(player.Id, context);

        if (depot is null) return;

        await ContainerManager.Save<Server.Entities.CharacterDepotItem>(player, depot, context);
        await context.SaveChangesAsync();
    }

    #endregion
}