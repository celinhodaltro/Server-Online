﻿using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Player;

public class PlayerStatusPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public PlayerStatusPacket(IPlayer player)
    {
        this.player = player;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.PlayerStatus);
        message.AddUInt16((ushort)Math.Min(ushort.MaxValue, player.HealthPoints));
        message.AddUInt16((ushort)Math.Min(ushort.MaxValue, player.MaxHealthPoints));
        message.AddUInt32((uint)player.CarryStrength * 100);

        message.AddUInt32(Math.Min(0x7FFFFFFF,
            player.Experience)); // Experience: Client debugs after 2,147,483,647 exp

        message.AddUInt16(player.Level);
        message.AddByte(player.LevelPercent);
        message.AddUInt16(Math.Min(ushort.MaxValue, player.Mana));
        message.AddUInt16(Math.Min(ushort.MaxValue, player.MaxMana));
        message.AddByte((byte)player.GetSkillLevel(SkillType.Magic));
        message.AddByte(player.GetSkillPercent(SkillType.Magic));

        message.AddByte(player.SoulPoints);
        message.AddUInt16(player.StaminaMinutes);
    }
}