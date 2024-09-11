using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Seeds;

internal sealed class PlayerItemSeed
{
    public static void Seed(EntityTypeBuilder<Server.Entities.PlayerItem> builder)
    {
        builder.HasData(
            new Server.Entities.PlayerItem
            {
                Id = -1,
                ContainerId = 1,
                PlayerId = 1,
                ParentId = 0,
                ServerId = 1988,
                Amount = 1
            },
            new Server.Entities.PlayerItem
            {
                Id = -2,
                PlayerId = 1,
                ParentId = 0,
                ServerId = 2666,
                Amount = 10
            },
            new Server.Entities.PlayerItem
            {
                Id = -3,
                PlayerId = 1,
                ParentId = 0,
                ServerId = 7618,
                Amount = 10
            },
            new Server.Entities.PlayerItem
            {
                Id = -4,
                PlayerId = 1,
                ParentId = 0,
                ServerId = 2311,
                Amount = 10
            },
            new Server.Entities.PlayerItem
            {
                Id = -5,
                PlayerId = 1,
                ParentId = 1,
                ServerId = 2304,
                Amount = 10
            }
        );
    }
}