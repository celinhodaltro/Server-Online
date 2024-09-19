using Game.Common.Creatures.Players;
using Game.World;

namespace Server.Entities;

public sealed class Character : DefaultDb
{
    public Character()
    {
        CharacterInventoryItems = new List<CharacterInventoryItem>();
        CharacterDepotItems = new List<CharacterDepotItem>();
        CharacterItems = new List<CharacterItem>();
        CharacterDepotItems = new List<CharacterDepotItem>();
    }

    public int UserId { get; set; }
    public int TownId { get; set; }
    public string Name { get; set; }
    public int PlayerType { get; set; }
    public uint Capacity { get; set; }
    public ushort Level { get; set; }
    public ushort Mana { get; set; }
    public ushort MaxMana { get; set; }
    public uint Health { get; set; }
    public uint MaxHealth { get; set; }
    public byte Soul { get; set; }
    public byte MaxSoul { get; set; }
    public ushort Speed { get; set; }
    public ushort StaminaMinutes { get; set; }
    public bool Online { get; set; }

    public int LookAddons { get; set; }
    public int LookBody { get; set; }
    public int LookFeet { get; set; }
    public int LookHead { get; set; }
    public int LookLegs { get; set; }
    public int LookType { get; set; }

    public int PosX { get; set; }
    public int PosY { get; set; }
    public int PosZ { get; set; }
    public double Experience { get; set; }

    public ChaseMode ChaseMode { get; set; }
    public FightMode FightMode { get; set; }
    public Gender Gender { get; set; }
    public byte Vocation { get; set; }
    public int RemainingRecoverySeconds { get; set; }
    public User Account { get; set; }

    public ICollection<CharacterSkill> CharacterSkills { get; set; }
    public ICollection<CharacterItem> CharacterItems { get; set; }
    public ICollection<CharacterDepotItem> CharacterDepotItems { get; set; }
    public ICollection<CharacterInventoryItem> CharacterInventoryItems { get; set; }
    public GuildMembership GuildMember { get; set; }
    public World World { get; set; }
    public int WorldId { get; set; }
}


public enum AccountType
{
    Character,
    Tutor,
    SeniorTutor,
    GameMaster,
    God
}

