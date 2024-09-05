using Autofac;
using Server.Jobs.Channels;
using Server.Jobs.Creatures;
using Server.Jobs.Items;
using Server.Jobs.Persistence;

namespace Server.Standalone.IoC.Modules;

public static class JobInjection
{
    public static ContainerBuilder AddJobs(this ContainerBuilder builder)
    {
        //todo: inherit these jobs from interface and register by implementation
        builder.RegisterType<GameCreatureJob>().SingleInstance();
        builder.RegisterType<GameItemJob>().SingleInstance();
        builder.RegisterType<GameChatChannelJob>().SingleInstance();
        builder.RegisterType<PlayerPersistenceJob>().SingleInstance();
        return builder;
    }
}