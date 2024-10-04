using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Seeds;

internal sealed class PlayerItemSeed
{
    public static void Seed(EntityTypeBuilder<Server.Entities.CharacterItem> builder)
    {
        builder.HasData(
            new Server.Entities.CharacterItem
            {
                Id = -1,
                ContainerId = 1,
                CharacterId = 1,
                ParentId = 0,
                ServerId = 1988,
                Amount = 1
            },
            new Server.Entities.CharacterItem
            {
                Id = -2,
                CharacterId = 1,
                ParentId = 0,
                ServerId = 2666,
                Amount = 10
            },
            new Server.Entities.CharacterItem
            {
                Id = -3,
                CharacterId = 1,
                ParentId = 0,
                ServerId = 7618,
                Amount = 10
            },
            new Server.Entities.CharacterItem
            {
                Id = -4,
                CharacterId = 1,
                ParentId = 0,
                ServerId = 2311,
                Amount = 10
            },
            new Server.Entities.CharacterItem
            {
                Id = -5,
                CharacterId = 1,
                ParentId = 1,
                ServerId = 2304,
                Amount = 10
            }
        );
    }
}