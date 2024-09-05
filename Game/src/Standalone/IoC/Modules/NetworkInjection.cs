using Autofac;
using Networking.Handlers.ClientVersion;
using Networking.Listeners;
using Networking.Protocols;

namespace Server.Standalone.IoC.Modules;

public static class NetworkInjection
{
    public static ContainerBuilder AddNetwork(this ContainerBuilder builder)
    {
        builder.RegisterType<LoginProtocol>().SingleInstance();
        builder.RegisterType<GameProtocol>().SingleInstance();
        builder.RegisterType<LoginListener>().SingleInstance();
        builder.RegisterType<GameListener>().SingleInstance();
        builder.RegisterType<ClientProtocolVersion>().SingleInstance();
        return builder;
    }
}