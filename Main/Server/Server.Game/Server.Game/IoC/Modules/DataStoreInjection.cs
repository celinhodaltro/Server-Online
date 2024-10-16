﻿using Autofac;
using Data.InMemory;
using Server.Entities.Common.Contracts;

namespace Server.Start.IoC.Modules;

public static class DataStoreInjection
{
    public static ContainerBuilder AddDataStores(this ContainerBuilder builder)
    {
        builder.RegisterType<ItemTypeStore>()
            .As<IItemTypeStore>()
            .SingleInstance();

        builder.RegisterType<ChatChannelStore>()
            .As<IChatChannelStore>()
            .SingleInstance();

        builder.RegisterType<GuildStore>()
            .As<IGuildStore>()
            .SingleInstance();

        builder.RegisterType<NpcStore>()
            .As<INpcStore>()
            .SingleInstance();

        builder.RegisterType<VocationStore>()
            .As<IVocationStore>()
            .SingleInstance();

        builder.RegisterType<CoinTypeStore>()
            .As<ICoinTypeStore>()
            .SingleInstance();

        builder.RegisterType<AreaEffectStore>()
            .As<IAreaEffectStore>()
            .SingleInstance();

        builder.RegisterType<PlayerOutFitStore>()
            .As<IPlayerOutFitStore>()
            .SingleInstance();

        builder.RegisterType<QuestDataDataStore>()
            .As<IQuestDataStore>()
            .SingleInstance();

        builder.RegisterType<ItemClientServerIdMapStore>()
            .As<IItemClientServerIdMapStore>()
            .SingleInstance();

        return builder;
    }
}