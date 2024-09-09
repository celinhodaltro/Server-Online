using Server.Entities.Contracts.Items;

namespace Server.Entities.Contracts.Services;

public interface IDecayService
{
    void Decay(IItem item);
}