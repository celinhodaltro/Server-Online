using Moq;
using Data.InMemory;
using Game.Common.Chats;
using Game.Common.Contracts.Creatures;
using Networking.Packets.Incoming;
using Server.Commands.Player;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Xunit;

namespace Server.Tests.Commands;

public class PlayerSayCommandTest
{
    [Fact]
    public void Execute_When_Player_Send_Message_To_Another_Player_Should_Call_Player_Method_Once()
    {
        //arrange
        var player = new Mock<IPlayer>();
        var connection = new Mock<IConnection>();
        var network = new Mock<IReadOnlyNetworkMessage>();

        var playerSayPacket = new Mock<PlayerSayPacket>(network.Object);
        playerSayPacket.SetupGet(x => x.TalkType).Returns(SpeechType.Private);
        playerSayPacket.SetupGet(x => x.Receiver).Returns("receiver");
        playerSayPacket.SetupGet(x => x.Message).Returns("hello");

        var chatChannelStore = new ChatChannelStore();

        var receiverMock = new Mock<IPlayer>();
        var receiver = receiverMock.Object;

        var game = new Mock<IGameServer>();
        game.Setup(x => x.CreatureManager.TryGetPlayer("receiver", out receiver)).Returns(true);

        var sut = new PlayerSayCommand(game.Object, chatChannelStore);

        //act
        sut.Execute(player.Object, connection.Object, playerSayPacket.Object);

        //assert
        player.Verify(x => x.SendMessageTo(receiver, SpeechType.Private, "hello"), Times.Once());
    }
}