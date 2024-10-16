﻿using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location.Structs;
using Game.Creatures.Models.Bases;
using Game.Creatures.Npcs.Dialogs;

namespace Game.Creatures.Npcs;

public class Npc : WalkableCreature, INpc
{
    public readonly Dictionary<string, Func<string, INpc, ISociableCreature, string>> KeywordReplacementMap;
    private readonly NpcDialog npcDialog;

    public Npc(INpcType type, IMapTool mapTool, ISpawnPoint spawnPoint, IOutfit outfit = null,
        uint healthPoints = 0) : base(type,
        mapTool, outfit, healthPoints)
    {
        Metadata = type;
        npcDialog = new NpcDialog(this);
        SpawnPoint = spawnPoint;

        Cooldowns.Start(CooldownType.Advertise, 10_000);
        Cooldowns.Start(CooldownType.WalkAround, 5_000);

        KeywordReplacementMap = new Dictionary<string, Func<string, INpc, ISociableCreature, string>>
        {
            ["|PLAYERNAME|"] = (_, _, player) => player.Name
        };
    }

    public CreateItem CreateNewItem { protected get; init; }

    public override ITileEnterRule TileEnterRule => NpcEnterTileRule.Rule;
    public KeywordReplacement ReplaceKeywords { get; set; }

    public ISpawnPoint SpawnPoint { get; }
    public override IOutfit Outfit { get; protected set; }
    public INpcType Metadata { get; }

    public override bool CanSeeInvisible => false;

    public override bool CanBeSeen => true;

    public Dictionary<string, string> GetPlayerStoredValues(ISociableCreature sociableCreature)
    {
        return npcDialog.GetDialogStoredValues(sociableCreature);
    }

    public void Advertise()
    {
        if (!Metadata.Marketings?.Any() ?? true) return;

        if (!Cooldowns.Cooldowns[CooldownType.Advertise].Expired) return;
        Say(GameRandom.Random.Next(Metadata.Marketings), SpeechType.Say);
        Cooldowns.Start(CooldownType.Advertise, 10_000);
    }

    public void BackInDialog(ISociableCreature creature, byte count)
    {
        npcDialog.Back(creature.CreatureId, count);
    }

    public override bool WalkRandomStep()
    {
        if (!Cooldowns.Cooldowns[CooldownType.WalkAround].Expired) return false;

        var result = base.WalkRandomStep(SpawnPoint.Location);

        Cooldowns.Start(CooldownType.WalkAround, (int)Metadata.WalkInterval);
        return result;
    }

    public override bool CanSee(Location pos)
    {
        return base.CanSee(pos, 3, 3);
    }

    public void Hear(ICreature from, SpeechType speechType, string message)
    {
        if (from is null || speechType == SpeechType.None || string.IsNullOrWhiteSpace(message)) return;

        OnHear?.Invoke(from, this, speechType, message);

        Answer(from, speechType, message);
    }

    public void StopTalkingToCustomer(IPlayer player)
    {
        npcDialog.StopTalkingTo(player);
    }

    public void ForgetCustomer(ISociableCreature sociableCreature)
    {
        StopWatchCustomerMovements(sociableCreature);
        npcDialog.EraseDialog(sociableCreature.CreatureId);
    }

    private string BindAnswerVariables(ISociableCreature creature, IDialog dialog, string answer)
    {
        var storedValues = npcDialog.GetDialogStoredValues(creature);
        if (string.IsNullOrWhiteSpace(dialog.StoreAt)) return answer;

        if (!storedValues.TryGetValue(dialog.StoreAt, out var value)) return answer;
        return answer.Replace($"{{{{{dialog.StoreAt}}}}}", value);
    }

    public virtual void Answer(ICreature from, SpeechType speechType, string message)
    {
        if (from is null || string.IsNullOrWhiteSpace(message)) return;

        if (from is not ISociableCreature sociableCreature) return;

        var isTalkingWith = npcDialog.IsTalkingWith(from);

        //if it is not the first message to npc and player sent it from any other channel
        if (isTalkingWith && speechType != SpeechType.PrivatePlayerToNpc) return;

        var dialog = npcDialog.GetNextAnswer(from.CreatureId, message);

        if (dialog is null) return;

        if (!isTalkingWith) WatchCustomerEvents(sociableCreature); //first interaction

        npcDialog.StoreWords(sociableCreature, dialog.StoreAt, message);

        if (dialog.Action is not null)
            OnDialogAction?.Invoke(this, from, dialog, dialog.Action, GetPlayerStoredValues(sociableCreature));

        if (dialog?.Answers is not null)
        {
            SendMessageTo(sociableCreature, speechType, dialog);

            OnAnswer?.Invoke(this, from, dialog, message, speechType);
        }

        if (dialog.End) ForgetCustomer(sociableCreature);
    }

    public virtual void SendMessageTo(ISociableCreature to, SpeechType type, IDialog dialog)
    {
        if (dialog?.Answers is null || to is null) return;

        foreach (var answer in dialog.Answers)
        {
            var replacedAnswer = ReplaceKeywords?.Invoke(answer, this, to) ?? answer;

            foreach (var keywordReplacement in KeywordReplacementMap)
                replacedAnswer = replacedAnswer.Replace(keywordReplacement.Key,
                    keywordReplacement.Value?.Invoke(null, this, to));

            var bindedAnswer = BindAnswerVariables(to, dialog, replacedAnswer);

            if (string.IsNullOrWhiteSpace(bindedAnswer) || to is not IPlayer) continue;

            Say(bindedAnswer, SpeechType.PrivateNpcToPlayer, to);
        }
    }

    private void WatchCustomerEvents(ISociableCreature creature)
    {
        creature.OnCreatureMoved += OnCustomerMoved;
        if (creature is IPlayer player) player.OnLoggedOut += HandleWhenCustomerLeave;
    }

    private void StopWatchCustomerMovements(ISociableCreature creature)
    {
        creature.OnCreatureMoved -= OnCustomerMoved;
        if (creature is IPlayer player) player.OnLoggedOut -= HandleWhenCustomerLeave;
    }

    private void OnCustomerMoved(ICreature creature, Location fromLocation, Location toLocation,
        ICylinderSpectator[] spectators)
    {
        if (CanSee(creature.Location)) return;
        HandleWhenCustomerLeave(creature);
    }

    private void HandleWhenCustomerLeave(ICreature creature)
    {
        if (creature is not ISociableCreature sociableCreature) return;

        ForgetCustomer(sociableCreature);
        OnCustomerLeft?.Invoke(creature);
    }

    #region Events

    public event DialogAction OnDialogAction;
    public event Answer OnAnswer;
    public event Hear OnHear;
    public event CustomerLeft OnCustomerLeft;

    #endregion
}