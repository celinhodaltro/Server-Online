﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Creatures.Structs;
using Server.Entities.Common.Effects.Parsers;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Parsers;

namespace Game.Combat.Conditions;

public class DamageCondition : BaseCondition
{
    private CooldownTime _cooldown;
    private Queue<ushort> _damageQueue;
    private ushort _maxDamage;
    private ushort _minDamage;

    public DamageCondition(ConditionType type, int interval, ushort minDamage, ushort maxDamage,
        EffectT effect = EffectT.None) : base(0)
    {
        Type = type;
        Interval = interval;
        DamageType = ConditionTypeParser.Parse(type);
        _maxDamage = maxDamage;
        _minDamage = minDamage;
        Effect = effect;
    }

    public DamageCondition(ConditionType type, int interval, byte amount, ushort damage,
        EffectT effect = EffectT.None) : base(0)
    {
        if (amount == 0) return;

        Type = type;
        Interval = interval;
        DamageType = ConditionTypeParser.Parse(type);
        _maxDamage = damage;
        _minDamage = damage;
        Effect = effect;
        Amount = amount;
    }

    public byte Amount { get; }
    public override ConditionType Type { get; }
    public DamageType DamageType { get; set; }
    public EffectT Effect { get; }

    public int Interval
    {
        set => _cooldown = new CooldownTime(DateTime.Now, value);
    }

    public override bool HasExpired => _damageQueue.Count <= 0;

    public void Execute(ICombatActor creature)
    {
        if (!_cooldown.Expired) return;

        _cooldown.Reset();
        if (!_damageQueue.TryDequeue(out var damage))
        {
            End();
            return;
        }

        creature.ReceiveAttack(null, new CombatDamage(damage, DamageType, DamageEffectParser.Parse(DamageType)));
    }

    public bool Start(ICreature creature, ushort minDamage, ushort maxDamage)
    {
        if (maxDamage < _maxDamage) return false;

        _minDamage = minDamage;
        _maxDamage = maxDamage;

        Start(creature);
        return true;
    }

    public override bool Start(ICreature creature)
    {
        if (Amount == 0)
            GenerateDamageList();
        else
            GenerateDamageList(Amount);

        base.Start(creature);
        return true;
    }

    public bool Restart(byte amount)
    {
        GenerateDamageList(amount);

        return true;
    }

    private int GetStartDamage(int maxDamage, int amount)
    {
        var startDamage = 0;
        if (startDamage > maxDamage)
            startDamage = maxDamage;
        else if (startDamage == 0) startDamage = (int)Math.Max(1, Math.Ceiling(amount / 20.0));
        return startDamage;
    }

    private void GenerateDamageList(byte amount)
    {
        _damageQueue ??= new Queue<ushort>();
        for (var i = 0; i < amount - _damageQueue.Count; i++) _damageQueue.Enqueue(_maxDamage);
    }

    private void GenerateDamageList()
    {
        if (_damageQueue is null) _damageQueue = new Queue<ushort>();

        int amount = (ushort)GameRandom.Random.Next(_minDamage, maxValue: _maxDamage);
        var start = GetStartDamage(_maxDamage, amount);

        _damageQueue.Clear();

        amount = Math.Abs(amount);
        var sum = 0;
        double x1, x2;

        for (var i = start; i > 0; --i)
        {
            var n = start + 1 - i;
            var med = n * amount / start;

            do
            {
                sum += i;
                _damageQueue.Enqueue((ushort)i);

                x1 = Math.Abs(1.0 - ((float)sum + i) / med);
                x2 = Math.Abs(1.0 - (float)sum / med);
            } while (x1 < x2);
        }
    }
}