using Extensions.Services;
using Server.Contracts.Contracts;
using NLua;

namespace Extensions;

public class Startup : IStartup
{
    private readonly Lua _lua;

    public Startup(Lua lua)
    {
        _lua = lua;
    }

    public void Run()
    {
        _lua["sendEffect"] = EffectService.Send;
    }
}