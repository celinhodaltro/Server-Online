﻿using System.Collections.Generic;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Services;
using Server.Entities.Common.Texts;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Creatures.Players;

namespace Game.Creatures.Player;

public class Vip : IVip
{
    private readonly IPlayer _owner;

    public Vip(IPlayer owner)
    {
        _owner = owner;
    }

    public HashSet<uint> VipList { get; set; } = new();

    public void LoadVipList(IEnumerable<(uint, string)> vips)
    {
        if (Guard.AnyNull(vips)) return;
        var vipList = new HashSet<(uint, string)>();
        foreach (var vip in vips)
        {
            if (string.IsNullOrWhiteSpace(vip.Item2)) continue;

            VipList.Add(vip.Item1);
            vipList.Add(vip);
        }

        OnLoadedVipList?.Invoke(_owner, vipList);
    }

    public bool AddToVip(IPlayer player)
    {
        if (Guard.AnyNull(player)) return false;
        if (string.IsNullOrWhiteSpace(player.Name)) return false;

        VipList ??= new HashSet<uint>();

        if (VipList.Count >= 200)
        {
            OperationFailService.Send(_owner.CreatureId, "You cannot add more buddies.");
            return false;
        }

        if (player.FlagIsEnabled(PlayerFlag.SpecialVip))
            if (!_owner.FlagIsEnabled(PlayerFlag.SpecialVip))
            {
                OperationFailService.Send(_owner.CreatureId, TextConstants.CANNOT_ADD_PLAYER_TO_VIP_LIST);
                return false;
            }

        if (!VipList.Add(player.Id))
        {
            OperationFailService.Send(_owner.CreatureId, "This player is already in your list.");
            return false;
        }

        OnAddedToVipList?.Invoke(_owner, player.Id, player.Name);
        return true;
    }

    public void RemoveFromVip(uint playerId)
    {
        VipList?.Remove(playerId);
    }

    public bool HasInVipList(uint playerId)
    {
        return VipList.Contains(playerId);
    }

    public event AddToVipList OnAddedToVipList;
    public event PlayerLoadVipList OnLoadedVipList;
}