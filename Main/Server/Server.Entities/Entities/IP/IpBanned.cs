using Server.Entities;
using System;

namespace Server.Entities.Entities.IP;

public class IpBanned : DefaultDb
{
    public uint Ip { get; set; }
    public string Reason { get; set; }
    public TimeSpan BannedAt { get; set; }
    public TimeSpan ExpiresAt { get; set; }
    public ushort BannedBy { get; set; }
}