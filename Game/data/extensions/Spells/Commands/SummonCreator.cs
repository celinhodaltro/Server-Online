using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Services;
using Game.Common.Contracts.World.Tiles;
using Game.Common.Location;
using Game.Creatures.Factories;
using Game.World.Map;
using Server.Helpers;

namespace Extensions.Spells.Commands;

public class SummonCreator : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotPossible;
        if (Params?.Length == 0) return false;

        var summon = CreatureFactory.Instance.CreateSummon(Params[0].ToString(), actor as IMonster);
        if (summon is null) return false;

        var map = Map.Instance;

        var tileToBorn = map[actor.Location.GetNextLocation(actor.Direction)];

        summon.SetNewLocation(tileToBorn.Location);

        if (tileToBorn is IDynamicTile)
        {
            if (tileToBorn.HasFlag(TileFlags.ProtectionZone))
            {
                error = InvalidOperation.NotEnoughRoom;
                return false;
            }

            map.PlaceCreature(summon);
            return true;
        }

        foreach (var neighbour in actor.Location.Neighbours)
            if (map[neighbour] is IDynamicTile { HasCreature: false })
            {
                map.PlaceCreature(summon);
                return true;
            }

        error = InvalidOperation.NotEnoughRoom;
        return false;
    }
}