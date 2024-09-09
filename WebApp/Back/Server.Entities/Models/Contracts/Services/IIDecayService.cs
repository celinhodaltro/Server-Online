using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.Services;

public interface IDecayService
{
    void Decay(IItem item);
}