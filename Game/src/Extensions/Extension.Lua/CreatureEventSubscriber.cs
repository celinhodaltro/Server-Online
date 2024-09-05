using System;
using System.IO;
using Game.Common.Contracts;
using Game.Common.Contracts.Creatures;
using Server.Configurations;

namespace Extension.Lua;

public class CreatureEventSubscriber : ICreatureEventSubscriber, IGameEventSubscriber
{
    private readonly NLua.Lua _lua;
    private readonly ServerConfiguration _serverConfiguration;

    public CreatureEventSubscriber(ServerConfiguration serverConfiguration, NLua.Lua lua)
    {
        _serverConfiguration = serverConfiguration;
        _lua = lua;
    }

    public void Subscribe(ICreature creature)
    {
        if (creature is not INpc npc || !npc.Metadata.IsLuaScript ||
            string.IsNullOrWhiteSpace(npc.Metadata.Script)) return;

        var script = Path.Combine(_serverConfiguration.Data, "npcs", "scripts", npc.Metadata.Script);

        _lua.DoFile(script);
        _lua.GetFunction("init").Call(creature);
    }

    public void Unsubscribe(ICreature creature)
    {
        throw new NotImplementedException();
    }
}