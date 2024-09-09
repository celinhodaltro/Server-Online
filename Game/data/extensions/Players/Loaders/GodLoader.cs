using Data.Entities;
using Game.Chats;
using Server.Entities;
using Server.Entities.Contracts.Creatures;
using Server.Entities.Contracts.DataStores;
using Server.Entities.Contracts.Items;
using Server.Entities.Contracts.World;
using Server.Entities.Helpers;
using Server.Entities.Location.Structs;
using Game.Creatures.Player;
using Game.World;
using Loader.Players;
using Serilog;

namespace Extensions.Players.Loader;

public class GodLoader : PlayerLoader
{
    public GodLoader(IItemFactory itemFactory, ICreatureFactory creatureFactory,
        ChatChannelFactory chatChannelFactory, IGuildStore guildStore,
        IVocationStore vocationStore, IMapTool mapTool, World world, ILogger logger,
        GameConfiguration gameConfiguration) :
        base(itemFactory, creatureFactory, chatChannelFactory, guildStore,
            vocationStore, mapTool, world, logger, gameConfiguration)
    {
    }

    public override bool IsApplicable(PlayerEntity player)
    {
        return player?.PlayerType == 3;
    }

    public override IPlayer Load(PlayerEntity playerEntity)
    {
        if (Guard.IsNull(playerEntity)) return null;

        var town = GetTown(playerEntity);

        var newPlayer = new God(
            (uint)playerEntity.Id,
            playerEntity.Name,
            VocationStore.Get(playerEntity.Vocation),
            playerEntity.Gender,
            playerEntity.Online,
            ConvertToSkills(playerEntity),
            new Outfit
            {
                Addon = (byte)playerEntity.LookAddons,
                Body = (byte)playerEntity.LookBody,
                Feet = (byte)playerEntity.LookFeet,
                Head = (byte)playerEntity.LookHead,
                Legs = (byte)playerEntity.LookLegs,
                LookType = (byte)playerEntity.LookType
            },
            playerEntity.Speed,
            new Location((ushort)playerEntity.PosX, (ushort)playerEntity.PosY, (byte)playerEntity.PosZ),
            MapTool,
            town)
        {
            AccountId = (uint)playerEntity.AccountId,
            Guild = GuildStore.Get((ushort)(playerEntity.GuildMember?.GuildId ?? 0)),
            GuildLevel = (ushort)(playerEntity.GuildMember?.RankId ?? 0)
        };

        SetCurrentTile(newPlayer);

        newPlayer.AddInventory(ConvertToInventory(newPlayer, playerEntity));
        var god = CreatureFactory.CreatePlayer(newPlayer);

        return god;
    }
}