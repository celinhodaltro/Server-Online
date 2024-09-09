using System;
using System.Collections.Generic;

namespace Data.Entities;

public sealed class Guild
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Modt { get; set; }
    public PlayerEntity Owner { get; set; }
    public ICollection<GuildMembership> Members { get; set; }
    public ICollection<GuildRank> Ranks { get; set; }
}