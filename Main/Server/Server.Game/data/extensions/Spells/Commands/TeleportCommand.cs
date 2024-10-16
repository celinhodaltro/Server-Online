﻿using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Services;
using Server.Entities.Common.Texts;
using Game.World.Map;

namespace Extensions.Spells.Commands;

public class TeleportCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotEnoughRoom;

        var steps = (Params?.Length ?? 0) == 0 ? 1 : int.Parse(Params[0].ToString());
        var newLocation = actor.Location.GetNextLocation(actor.Direction, (ushort)steps);

        if (Map.Instance[newLocation] is not IDynamicTile)
        {
            OperationFailService.Send(actor.CreatureId, TextConstants.NOT_ENOUGH_ROOM);
            return false;
        }

        actor.TeleportTo(newLocation);
        return true;
    }
}