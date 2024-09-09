namespace Server.Entities.Models.Creatures;

public enum CreatureFlagAttribute : byte
{
    Summonable,
    Attackable,
    Hostile,
    Illusionable,
    Convinceable,
    Pushable,
    CanPushItems,
    CanPushCreatures,
    TargetDistance,
    StaticAttack,
    RunOnHealth,
    None,
    LightColor,
    IsBoss,
    RewardBoss
}