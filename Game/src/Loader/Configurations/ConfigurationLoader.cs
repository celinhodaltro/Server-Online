using Data.InMemory;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Loader.Interfaces;

namespace Loader.Configurations;

public class ConfigurationLoader : IStartupLoader
{
    private readonly IPathFinder pathFinder;
    private readonly IWalkToMechanism walkToMechanism;

    public ConfigurationLoader(IPathFinder pathFinder, IWalkToMechanism walkToMechanism)
    {
        this.pathFinder = pathFinder;
        this.walkToMechanism = walkToMechanism;
    }

    public void Load()
    {
        GameToolStore.PathFinder = pathFinder;
        GameToolStore.WalkToMechanism = walkToMechanism;
    }
}