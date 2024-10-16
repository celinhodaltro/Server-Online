using System;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Location.Structs;

namespace Server.Contracts.Contracts.Network;

public interface INetworkMessage : IReadOnlyNetworkMessage
{
    void AddByte(byte b);
    void AddBytes(ReadOnlySpan<byte> bytes);
    void AddPaddingBytes(int count);
    void AddString(string value);
    void AddUInt16(ushort value);
    void AddUInt32(uint value);
    byte[] AddHeader(bool addChecksum = true);
    void AddItem(IItem item);
    void AddLocation(Location location);
    void AddLength();
}