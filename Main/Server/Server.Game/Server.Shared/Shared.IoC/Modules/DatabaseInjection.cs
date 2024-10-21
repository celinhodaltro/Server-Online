﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Data.Contexts;
using Data.Factory;
using Data.Interfaces;
using Data.Providers.InMemory;
using Data.Providers.MySQL;
using Data.Providers.SQLite;
using Data.Repositories;
using Data.Repositories.Player;
using Server.Configurations;
using Server.BusinessRules;
using Autofac.Core;
using System.Configuration;
using System.Provider;

namespace Shared.IoC.Modules;

public static class DatabaseInjection
{
    public static ContainerBuilder AddRepositories(this ContainerBuilder builder)
    {
        builder.RegisterType<AccountRepository>().As<IAccountRepository>().SingleInstance();
        builder.RegisterType<GuildRepository>().As<IGuildRepository>().SingleInstance();
        builder.RegisterType<PlayerDepotItemRepository>().As<IPlayerDepotItemRepository>().SingleInstance();
        builder.RegisterType<PlayerRepository>().As<IPlayerRepository>().SingleInstance();

        builder.RegisterGeneric(typeof(BaseRepository<>));

        return builder;
    }


    public static ContainerBuilder AddContext(this ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
        return builder;

    }
    public static ContainerBuilder AddBusinessRules(this ContainerBuilder builder)
    {
        builder.RegisterType<CharacterBusinessRules>().SingleInstance();
        builder.RegisterType<UserBusinessRules>().SingleInstance();
        builder.RegisterType<LoggerBusinessRules>().SingleInstance();
        return builder;
    }

    public static ContainerBuilder AddProvider(this ContainerBuilder builder)
    {
        builder.RegisterType<CharacterProvider>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<DefaultProvider>().AsSelf().InstancePerLifetimeScope();
        return builder;
    }

    public static ContainerBuilder AddDatabases(this ContainerBuilder builder, IConfiguration configuration)
    {
        builder.RegisterContext<NeoContext>(configuration);
        return builder;
    }

    private static void RegisterContext<TContext>(this ContainerBuilder builder, IConfiguration configuration)
        where TContext : DbContext
    {
        DatabaseConfiguration config = new(null, DatabaseType.INMEMORY);

        configuration.GetSection("database").Bind(config);

        var options = config.Active switch
        {
            DatabaseType.INMEMORY => DbContextFactory.GetInstance()
                .UseInMemory(config.Connections[DatabaseType.INMEMORY]),
            DatabaseType.MONGODB => DbContextFactory.GetInstance()
                .UseInMemory(config.Connections[DatabaseType.MONGODB]),
            DatabaseType.MYSQL => DbContextFactory.GetInstance().UseMySql(config.Connections[DatabaseType.MYSQL]),
            DatabaseType.MSSQL => DbContextFactory.GetInstance()
                .UseInMemory(config.Connections[DatabaseType.MSSQL]),
            DatabaseType.SQLITE => DbContextFactory.GetInstance()
                .UseSQLite(config.Connections[DatabaseType.SQLITE]),
            _ => throw new ArgumentException("Invalid active database!")
        };

        builder.RegisterInstance(config).SingleInstance();

        builder.RegisterType<TContext>()
            .WithParameter("options", options)
            .InstancePerLifetimeScope();

        builder.RegisterInstance(options).SingleInstance();
    }
}