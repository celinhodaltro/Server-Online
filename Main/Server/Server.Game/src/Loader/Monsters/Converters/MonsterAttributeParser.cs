using Server.Entities.Common.Creatures;

namespace Loader.Monsters.Converters;

public class MonsterAttributeParser
{
    public static EffectT ParseAreaEffect(string type)
    {
        return type switch
        {
            "blueshimmer" => EffectT.GlitterBlue,
            "redshimmer" => EffectT.GlitterRed,
            "greenshimmer" => EffectT.GlitterGreen,
            "mortarea" => EffectT.BubbleBlack,
            "groundshaker" => EffectT.GroundShaker,
            _ => EffectT.None
        };
    }
}