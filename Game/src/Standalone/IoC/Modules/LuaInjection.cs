using Autofac;
using Extension.Lua;
using NLua;

namespace Server.Standalone.IoC.Modules;

public static class LuaInjection
{
    public static ContainerBuilder AddLua(this ContainerBuilder builder)
    {
        builder.RegisterInstance(new Lua()).SingleInstance();
        builder.RegisterType<LuaGlobalRegister>().SingleInstance();
        return builder;
    }
}