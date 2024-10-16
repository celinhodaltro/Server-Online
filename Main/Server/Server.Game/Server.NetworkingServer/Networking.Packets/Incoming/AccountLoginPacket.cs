﻿using Networking.Packets.Messages;
using Server.Common.Contracts.Network;
using Server.Security;

namespace Networking.Packets.Incoming;

public class AccountLoginPacket : IncomingPacket
{
    public AccountLoginPacket(IReadOnlyNetworkMessage message)
    {
        var packetPayload = message.GetUInt16();
        var tcpPayload = packetPayload + 2;
        message.SkipBytes(7);
        //var os = message.GetUInt16();
        ProtocolVersion = message.GetUInt16();

        message.SkipBytes(12);

        var encryptedDataLength = tcpPayload - message.BytesRead;
        var encryptedData = message.GetBytes(encryptedDataLength);
        var bytes = Rsa.Decrypt(encryptedData.ToArray());
        
        if (bytes is null || bytes.Length == 0) return;

        var data = new ReadOnlyNetworkMessage(bytes, encryptedDataLength);

        LoadXtea(data);

        Account = data.GetString();
        Password = data.GetString();
    }

    public string Account { get; }
    public string Password { get; }
    public ushort ProtocolVersion { get; }

    public bool IsValid()
    {
        return !(string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password));
    }
}