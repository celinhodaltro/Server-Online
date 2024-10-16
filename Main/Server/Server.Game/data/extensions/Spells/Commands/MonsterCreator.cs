using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Game.Creatures.Factories;
using Game.World.Map;

namespace Extensions.Spells.Commands;

public class MonsterCreator : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotPossible;
        if (Params?.Length == 0) return false;

        var monster = CreatureFactory.Instance.CreateMonster(Params[0].ToString());
        if (monster is null) return false;

        var map = Map.Instance;

        var locationToBorn = actor.Location.GetNextLocation(actor.Direction);
        var tileToBorn = map[actor.Location.GetNextLocation(actor.Direction)];

        if (tileToBorn is IDynamicTile)
        {
            if (tileToBorn.HasFlag(TileFlags.ProtectionZone))
            {
                error = InvalidOperation.NotEnoughRoom;
                return false;
            }

            monster.Born(locationToBorn);
            return true;
        }

        foreach (var neighbour in actor.Location.Neighbours)
            if (map[neighbour] is IDynamicTile { HasCreature: false })
            {
                monster.Born(neighbour);
                return true;
            }

        error = InvalidOperation.NotEnoughRoom;
        return false;
    }
}