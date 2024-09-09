using System;
using System.Collections.Generic;
using Game.Common.Location;
using Server.Entities.Models.Contracts;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.Contracts.World.Tiles;

public delegate void AddCreatureToTile(ICreature creature, ITile tile);

public interface IDynamicTile : ITile, IHasItem
{
    IGround Ground { get; }
    List<IWalkableCreature> Creatures { get; }
    ushort StepSpeed { get; }

    FloorChangeDirection FloorDirection { get; }
    bool HasCreature { get; }
    IMagicField MagicField { get; }

    bool HasBlockPathFinding { get; }
    bool HasHole { get; }
    List<IPlayer> Players { get; }
    Func<ICreature, bool> CanEnter { get; set; }
    IItem[] AllItems { get; }
    bool HasTeleport(out ITeleport teleport);

    byte[] GetRaw(IPlayer playerRequesting = null);
    ICreature GetTopVisibleCreature(ICreature creature);
    bool TryGetStackPositionOfItem(IItem item, out byte stackPosition);
    event AddCreatureToTile CreatureAdded;
    IItem[] RemoveAllItems();
    ICreature[] RemoveAllCreatures();
    bool HasCreatureOfType<T>() where T : ICreature;
    void ReplaceGround(IGround ground);
    IItem[] RemoveStaticItems();
    IItem RemoveItem(ushort id);
    void ReplaceItem(ushort fromId, IItem toItem);
    Result<IItem> RemoveTopItem(bool force = false);
    bool HasHeight(int totalHeight);
    void ReplaceItem(IItem fromItem, IItem toItem);
    bool UpdateItemType(IItem fromItem, IItemType toItemType);
    IItem RemoveItem(IItem item);
}