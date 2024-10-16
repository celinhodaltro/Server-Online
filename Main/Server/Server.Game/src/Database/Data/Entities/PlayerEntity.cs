using System.Collections.Generic;
using Server.Entities.Common.Characters;
using Server.Entities.Common.Creatures.Players;

namespace Data.Entities;

public sealed class PlayerEntity
{
    public PlayerEntity()
    {
        PlayerInventoryItems = new List<PlayerInventoryItemEntity>();
        PlayerDepotItems = new List<PlayerDepotItemEntity>();
        PlayerItems = new List<PlayerItemEntity>();
        PlayerDepotItems = new List<PlayerDepotItemEntity>();
    }

    public int Id { get; set; }
    public int AccountId { get; set; }
    public int TownId { get; set; }
    public string Name { get; set; }
    public int PlayerType { get; set; } //1 = player
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
    public int SkillFist { get; set; }
    public double SkillFistTries { get; set; }

    public int SkillClub { get; set; }
    public double SkillClubTries { get; set; }

    public int SkillSword { get; set; }
    public double SkillSwordTries { get; set; }

    public int SkillAxe { get; set; }
    public double SkillAxeTries { get; set; }

    public int SkillDist { get; set; }
    public double SkillDistTries { get; set; }

    public int SkillShielding { get; set; }
    public double SkillShieldingTries { get; set; }

    public int SkillFishing { get; set; }
    public double SkillFishingTries { get; set; }

    public int MagicLevel { get; set; }
    public double MagicLevelTries { get; set; }
    public double Experience { get; set; }

    public ChaseMode ChaseMode { get; set; }
    public FightMode FightMode { get; set; }
    public Gender Gender { get; set; }
    public byte Vocation { get; set; }
    public int RemainingRecoverySeconds { get; set; }
    public AccountEntity Account { get; set; }

    public ICollection<PlayerItemEntity> PlayerItems { get; set; }
    public ICollection<PlayerDepotItemEntity> PlayerDepotItems { get; set; }
    public ICollection<PlayerInventoryItemEntity> PlayerInventoryItems { get; set; }
    public GuildMembershipEntity GuildMember { get; set; }
    public WorldEntity World { get; set; }
    public int WorldId { get; set; }
}