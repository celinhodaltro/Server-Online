using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Items.Types;

public interface ISkillBonus
{
    void AddSkillBonus(IPlayer player);
    void RemoveSkillBonus(IPlayer player);
}