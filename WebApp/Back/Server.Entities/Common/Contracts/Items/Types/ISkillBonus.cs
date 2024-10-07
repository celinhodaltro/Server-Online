using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items.Types;

public interface ISkillBonus
{
    void AddSkillBonus(IPlayer player);
    void RemoveSkillBonus(IPlayer player);
}