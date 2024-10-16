﻿using System;
using System.Linq;
using Autofac;

namespace Server.Start.IoC;

public static class ContainerHelpers
{
    private static readonly Type[] AssemblyCache = Container.AssemblyCache.SelectMany(x => x.GetTypes()).ToArray();

    public static void RegisterAssembliesByInterface(this ContainerBuilder builder, Type interfaceType)
    {
        try
        {
             var types = AssemblyCache.Where(interfaceType.IsAssignableFrom);

            foreach (var type in types)
            {
                if (type == interfaceType) continue;
                builder.RegisterType(type).SingleInstance();
            }
        }
        catch( Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}