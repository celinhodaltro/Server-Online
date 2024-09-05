using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Items.Types;

public interface ISkillBonus
{
    void AddSkillBonus(IPlayer player);
    void RemoveSkillBonus(IPlayer player);
}