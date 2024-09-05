using Autofac;
using Server.Commands.Movements.ToContainer;
using Server.Commands.Movements.ToInventory;

namespace Server.Standalone.IoC.Modules;

public static class CommandInjection
{
    public static ContainerBuilder AddCommands(this ContainerBuilder builder)
    {
        builder.RegisterType<MapToContainerMovementOperation>().SingleInstance();
        builder.RegisterType<MapToInventoryMovementOperation>().SingleInstance();
        return builder;
    }
}