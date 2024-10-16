﻿using System.Collections.Generic;
using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures.Players;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.Spells;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Results;
using Server.Entities.Common.Characters;

namespace Server.Entities.Common.Contracts.Creatures;

public delegate void ChangeChaseMode(IPlayer player, ChaseMode oldChaseMode, ChaseMode newChaseMode);

public delegate void ClosedContainer(IPlayer player, byte containerId, IContainer container);

public delegate void ClosedDepot(IPlayer player, byte containerId, IDepot container);

public delegate void OpenedContainer(IPlayer player, byte containerId, IContainer container);

public delegate void ReduceMana(IPlayer player);

public delegate void CannotUseSpell(IPlayer player, ISpell spell, InvalidOperation error);

public delegate void PlayerLevelAdvance(IPlayer player, SkillType type, int fromLevel, int toLevel);

public delegate void PlayerLevelRegress(IPlayer player, SkillType type, int fromLevel, int toLevel);

public delegate void LookAt(IPlayer player, IThing thing, bool isClose);

public delegate void PlayerGainSkillPoint(IPlayer player, SkillType type);

public delegate void UseItem(IPlayer player, IThing thing, IUsableOn item);

public delegate void LogIn(IPlayer player);

public delegate void LogOut(IPlayer player);

public delegate void AddToVipList(IPlayer player, uint vipPlayerId, string vipPlayerName);

public delegate void PlayerLoadVipList(IPlayer player, IEnumerable<(uint, string)> vipList);

public delegate void ChangeOnlineStatus(IPlayer player, bool online);

public delegate void SendMessageTo(ISociableCreature from, ISociableCreature to, SpeechType speechType,
    string message);

public delegate void Exhaust(IPlayer player);

public delegate void AddSkillBonus(IPlayer player, SkillType skillType, sbyte increased);

public delegate void RemoveSkillBonus(IPlayer player, SkillType skillType, sbyte decreased);

public delegate void ReadText(IPlayer player, IReadable readable, string text);

public delegate void WroteText(IPlayer player, IReadable readable, string text);

public interface IPlayer : ICombatActor, ISociableCreature
{
    ushort Level { get; }

    byte LevelPercent { get; }

    uint Experience { get; }
    byte SoulPoints { get; }

    float CarryStrength { get; }

    ushort StaminaMinutes { get; }

    FightMode FightMode { get; }
    ChaseMode ChaseMode { get; }
    byte SecureMode { get; }

    new bool InFight { get; }
    IPlayerContainerList Containers { get; }

    ITown Town { get; }

    IInventory Inventory { get; }
    ushort Mana { get; }
    ushort MaxMana { get; }
    SkillType SkillInUse { get; }
    bool CannotLogout { get; }
    uint Id { get; }
    bool HasDepotOpened { get; }
    uint TotalCapacity { get; }
    bool Recovering { get; }
    IVocation Vocation { get; }
    byte VocationType => Vocation?.VocationType ?? default;
    uint AccountId { get; init; }
    IGuild Guild { get; }
    ushort GuildId => Guild?.Id ?? default;
    bool HasGuild { get; }
    bool Shopping { get; }
    ulong BankAmount { get; }
    IShopperNpc TradingWithNpc { get; }

    byte MaxSoulPoints { get; }
    IVip Vip { get; }
    IPlayerChannel Channels { get; set; }
    IPlayerParty PlayerParty { get; set; }
    string GenderPronoun { get; }
    Gender Gender { get; }
    int PremiumTime { get; }
    IDictionary<SkillType, ISkill> Skills { get; }

    bool CanSeeInspectionDetails { get; }
    ulong GetTotalMoney(ICoinTypeStore coinTypeStore);
    event UseSpell OnUsedSpell;
    event SendMessageTo OnSentMessage;

    event CannotUseSpell OnCannotUseSpell;
    event LookAt OnLookedAt;
    event PlayerGainSkillPoint OnGainedSkillPoint;
    event UseItem OnUsedItem;
    event ReduceMana OnStatusChanged;
    event PlayerLevelAdvance OnLevelAdvanced;
    event PlayerLevelRegress OnLevelRegressed;
    event LogIn OnLoggedIn;
    event LogOut OnLoggedOut;
    event ChangeOnlineStatus OnChangedOnlineStatus;
    event Exhaust OnExhausted;
    uint ChooseToRemoveFromKnownSet(); //todo: looks like implementation detail

    /// <summary>
    ///     Checks if player knows creature with given id
    /// </summary>
    /// <param name="creatureId"></param>
    /// <returns></returns>
    bool KnowsCreatureWithId(uint creatureId);

    /// <summary>
    ///     Get skillType info
    /// </summary>
    /// <param name="skillType"></param>
    /// <returns></returns>
    ushort GetSkillLevel(SkillType skillType);

    /// <summary>
    ///     Changes player's fight mode
    /// </summary>
    /// <param name="fightMode"></param>
    void ChangeFightMode(FightMode fightMode);

    /// <summary>
    ///     Changes player's chase mode
    /// </summary>
    /// <param name="chaseMode"></param>
    void ChangeChaseMode(ChaseMode chaseMode);

    /// <summary>
    ///     Toogle Secure Mode
    /// </summary>
    /// <param name="secureMode"></param>
    void ChangeSecureMode(byte secureMode);

    byte GetSkillPercent(SkillType type);

    void AddKnownCreature(uint creatureId);

    /// <summary>
    ///     Checks if the player has specified mana points
    /// </summary>
    /// <param name="mana"></param>
    /// <returns></returns>
    bool HasEnoughMana(ushort mana);

    /// <summary>
    ///     Consume mana points
    /// </summary>
    /// <param name="mana"></param>
    void ConsumeMana(ushort mana);

    /// <summary>
    ///     Checks if the player has specified level points
    /// </summary>
    /// <returns></returns>
    bool HasEnoughLevel(ushort level);

    bool Logout(bool forced = false);
    ushort CalculateAttackPower(float attackRate, ushort attack);
    void LookAt(ITile tile);
    void LookAt(byte containerId, sbyte containerSlot);
    void LookAt(Slot slot);

    /// <summary>
    ///     Health and mana recovery
    /// </summary>
    void Recover();

    void HealMana(ushort increasing);
    bool Feed(IFood food);

    Result Use(IUsableOn item, ITile tile);
    Result Use(IUsableOn item, ICreature onCreature);
    void Use(IThing item);
    Result Use(IUsableOn item, IItem onItem);
    bool Login();

    bool CastSpell(string message);

    bool FlagIsEnabled(PlayerFlag flag);
    void SendMessageTo(ISociableCreature creature, SpeechType type, string message);
    void StartShopping(IShopperNpc npc);
    void StopShopping();
    bool Sell(IItemType item, byte amount, bool ignoreEquipped);
    void ReceivePayment(IEnumerable<IItem> coins, ulong total);
    bool CanReceiveInCashPayment(IEnumerable<IItem> coins);
    void ReceivePurchasedItems(INpc from, SaleContract saleContract, params IItem[] items);
    void WithdrawFromBank(ulong amount);
    void LoadBank(ulong amount);
    void SetFlag(PlayerFlag flag);
    void UnsetFlag(PlayerFlag flag);
    byte GetSkillTries(SkillType skillType);
    void AddSkillBonus(SkillType skillType, sbyte increase);
    void RemoveSkillBonus(SkillType skillType, sbyte decrease);
    event AddSkillBonus OnAddedSkillBonus;
    event RemoveSkillBonus OnRemovedSkillBonus;
    sbyte GetSkillBonus(SkillType skill);
    void AddInventory(IInventory inventory);
    void Read(IReadable readable);
    event ReadText OnReadText;
    void Write(IReadable readable, string text);
    void StopAllActions();
    Result<OperationResultList<IItem>> PickItemFromGround(IItem item, ITile tile, byte amount = 1);

    Result<OperationResultList<IItem>> MoveItem(IItem item, IHasItem source, IHasItem destination, byte amount,
        byte fromPosition,
        byte? toPosition);

    bool CanUseOutfit(IOutfit outFit);
    void SetAsHungry();
    void Use(IContainer item, byte openAtIndex);
}