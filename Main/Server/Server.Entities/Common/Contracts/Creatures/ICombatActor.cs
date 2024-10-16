﻿using System.Collections.Generic;
using Server.Entities.Common.Combat;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Combat.Attacks;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.Spells;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Results;

namespace Server.Entities.Common.Contracts.Creatures;

public delegate void AttackTargetChange(ICombatActor actor, uint oldTargetId, uint newTargetId);

public delegate void Damage(IThing enemy, ICombatActor victim, CombatDamage damage);

public delegate void Attacked(IThing enemy, ICombatActor victim, ref CombatDamage damage);

public delegate void Heal(ICombatActor healedCreature, ICreature healingCreature, ushort amount);

public delegate void StopAttack(ICombatActor actor);

public delegate void BlockAttack(ICombatActor creature, BlockType block);

public delegate void Attack(ICombatActor creature, ICreature victim, CombatAttackResult[] combatAttacks);

public delegate void UseSpell(ICreature creature, ISpell spell);

public delegate void ChangeVisibility(ICombatActor actor);

public delegate void PropagateAttack(ICombatActor actor, CombatDamage damage, AffectedLocation[] area);

public delegate void DropLoot(ICombatActor actor, ILoot loot);

public interface ICombatActor : IWalkableCreature
{
    ushort ArmorRating { get; }
    bool Attacking { get; }
    uint AutoAttackTargetId { get; }
    decimal AttackSpeed { get; }
    decimal BaseDefenseSpeed { get; }

    bool InFight { get; }
    bool IsDead { get; }
    ushort MinimumAttackPower { get; }
    bool UsingDistanceWeapon { get; }
    uint AttackEvent { get; set; }
    bool CanBeAttacked { get; }
    IDictionary<ConditionType, ICondition> Conditions { get; set; }
    ICreature CurrentTarget { get; }
    event Attack OnAttackEnemy;
    event BlockAttack OnBlockedAttack;
    event Damage OnInjured;
    event Heal OnHeal;
    event Die OnKilled;
    event StopAttack OnStoppedAttack;
    event AttackTargetChange OnTargetChanged;
    event ChangeVisibility OnChangedVisibility;
    event PropagateAttack OnPropagateAttack;
    event GainExperience OnGainedExperience;
    event RemoveCondition OnRemovedCondition;
    event AddCondition OnAddedCondition;
    event Attacked OnAttacked;
    int DefendUsingArmor(int attack);
    Result Attack(ICombatActor enemy, ICombatAttack attack, CombatAttackValue value);
    void Heal(ushort increasing, ICreature healedBy);
    CombatDamage ReduceDamage(CombatDamage damage);
    Result SetAttackTarget(ICreature target);
    int DefendUsingShield(int attack);
    void StopAttack();
    void ResetHealthPoints();
    void TurnInvisible();
    void TurnVisible();
    void StartSpellCooldown(ISpell spell);
    bool SpellCooldownHasExpired(ISpell spell);
    bool CooldownHasExpired(CooldownType type);

    /// <summary>
    ///     Creature receive attack damage from enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    /// <returns>Returns true when damage was bigger than 0</returns>
    bool ReceiveAttack(IThing enemy, CombatDamage damage);

    Result Attack(ICombatActor creature);
    void PropagateAttack(AffectedLocation[] area, CombatDamage damage);
    bool Attack(ICreature creature, IUsableAttackOnCreature item);

    /// <summary>
    ///     Set creature as enemy. If monster can't see creature it will be forgotten
    /// </summary>
    void SetAsEnemy(ICreature actor);

    void GainExperience(long exp);
    void LoseExperience(long exp);
    void AddCondition(ICondition condition);
    void RemoveCondition(ICondition condition);
    void RemoveCondition(ConditionType type);
    bool HasCondition(ConditionType type, out ICondition condition);
    bool HasCondition(ConditionType type);
    void PropagateAttack(AffectedLocation area, CombatDamage damage);
    void OnEnemyAppears(ICombatActor enemy);
    bool IsHostileTo(ICombatActor enemy);
    Result OnAttack(ICombatActor enemy, out CombatAttackResult[] combatAttacks);
    event StopAttack OnAttackCanceled;
}