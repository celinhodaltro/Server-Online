﻿using Server.Entities.Common;
using Server.Entities.Common.Texts;

namespace Networking.Packets.Outgoing;

public static class TextMessageOutgoingParser
{
    public static string Parse(InvalidOperation error)
    {
        return error switch
        {
            InvalidOperation.NotEnoughRoom => "There is not enough room.",
            InvalidOperation.Impossible => "This is impossible",
            InvalidOperation.BothHandsNeedToBeFree => "Both hands need to be free.",
            InvalidOperation.CannotDress => "You cannot dress this object there.",
            InvalidOperation.NotPossible => TextConstants.NOT_POSSIBLE,
            InvalidOperation.PlayerNotFound => "Sorry, player is offline.",
            InvalidOperation.TooHeavy => "This object is too heavy for you to carry.",
            InvalidOperation.VocationCannotUseSpell => "Your vocation cannot use this spell.",
            InvalidOperation.NotEnoughMana => "You do not have enough mana.",
            InvalidOperation.NotEnoughLevel => "You do not have enough level.",
            InvalidOperation.Exhausted => "You are exhausted.",
            InvalidOperation.CreatureIsNotReachable => "Creature is not reachable.",
            InvalidOperation.CannotAttackThatFast => "You cannot attack that fast.",
            InvalidOperation.NotPermittedInProtectionZone => TextConstants.NOT_PERMITTED_IN_PROTECTION_ZONE,

            _ => string.Empty
        };
    }
}