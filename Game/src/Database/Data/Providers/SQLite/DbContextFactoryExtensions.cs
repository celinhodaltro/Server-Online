using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Factory;

namespace Data.Providers.SQLite;

public static class DbContextFactoryExtensions
{
    public static DbContextOptions<NeoContext> UseSQLite(this DbContextFactory factory, string name)
    {
        var builder = new DbContextOptionsBuilder<NeoContext>();
        builder.UseSqlite(name);

        return builder.Options;
    }
}