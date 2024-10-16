using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Factory;

namespace Data.Providers.InMemory;

public static class DbContextFactoryExtensions
{
    public static DbContextOptions<NeoContext> UseInMemory(this DbContextFactory factory, string name)
    {
        var builder = new DbContextOptionsBuilder<NeoContext>();
        builder.UseInMemoryDatabase(name);

        return builder.Options;
    }
}