﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Serilog;

namespace Data.Repositories;

public class GuildRepository : BaseRepository<GuildEntity>, IGuildRepository
{
    #region constructors

    public GuildRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    #endregion

    public async Task<IEnumerable<GuildEntity>> GetAll()
    {
        await using var context = NewDbContext;
        return await context.Guilds.Include(x => x.Members).ThenInclude(x => x.Rank).ToListAsync();
    }
}