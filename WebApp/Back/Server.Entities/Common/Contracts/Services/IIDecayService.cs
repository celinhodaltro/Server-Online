using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts.Services;

public interface IDecayService
{
    void Decay(IItem item);
}