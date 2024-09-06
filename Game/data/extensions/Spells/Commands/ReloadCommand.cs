using System;
using System.Collections.Generic;
using System.Linq;
using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Item;
using Game.Common.Services;
using Game.Items.Factories;
using Loader.Quest;
using Loader.Vocations;
using Extension.Lua;
using Extension.Lua.Functions;
using Server.Helpers;
using NLua;

namespace Extensions.Spells.Commands;

public class ReloadCommand : CommandSpell
{
    private static Dictionary<string, Action> Modules => new()
    {
        ["vocations"] = IoC.GetInstance<VocationLoader>().Reload,
        ["lua"] = IoC.GetInstance<LuaGlobalRegister>().Register,
        ["quests"] = () =>
        {
            IoC.GetInstance<QuestDataLoader>().Load();
            QuestFunctions.RegisterQuests(IoC.GetInstance<Lua>());
        }
    };

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotPossible;

        if (Params is null || !Params.Any())
        {
            OperationFailService.Send(actor.CreatureId, "Invalid module");
            return false;
        }

        Console.Write(Params.Length);

        var module = Params[0].ToString();

        if (!Modules.TryGetValue(module, out var action))
        {
            OperationFailService.Send(actor.CreatureId, "Invalid module");
            return false;
        }

        action?.Invoke();
        return true;
    }

    private IItem Item(ICombatActor actor, int amount)
    {
        if (ushort.TryParse(Params[0].ToString(), out var typeId))
            return ItemFactory.Instance.Create(typeId, actor.Location,
                new Dictionary<ItemAttribute, IConvertible> { { ItemAttribute.Count, amount } });

        var item = ItemFactory.Instance.Create(Params[0].ToString(), actor.Location,
            new Dictionary<ItemAttribute, IConvertible> { { ItemAttribute.Count, amount } });

        return item;
    }
}