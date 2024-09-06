using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces;

public interface IGuildRepository : IBaseRepositoryNeo<GuildEntity>
{
    Task<IEnumerable<GuildEntity>> GetAll();
}