using Data.Interfaces;
using Networking.Handlers.ClientVersion;
using Networking.Packets.Incoming;
using Networking.Packets.Outgoing.Login;
using Server.BusinessRules;
using Server.Common.Contracts.Network;
using Server.Configurations;

namespace Networking.Handlers.LogIn;

public class AccountLoginHandler : PacketHandler
{
    private readonly ClientProtocolVersion _clientProtocolVersion;
    private readonly UserBusinessRules UserBusinessRules;
    private readonly ServerConfiguration _serverConfiguration;

    public AccountLoginHandler(IAccountRepository repositoryNeo, ServerConfiguration serverConfiguration,
        ClientProtocolVersion clientProtocolVersion, UserBusinessRules UserBusinessRules)
    {
        UserBusinessRules = UserBusinessRules;
        _serverConfiguration = serverConfiguration;
        _clientProtocolVersion = clientProtocolVersion;
    }

    public override async void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var account = new AccountLoginPacket(message);

        if (!_clientProtocolVersion.IsSupported(account.ProtocolVersion))
        {
            connection.Close();
            return;
        }

        connection.SetXtea(account.Xtea);

        if (account == null)
        {
            //todo: use option
            connection.Disconnect("Invalid account.");
            return;
        }

        if (!account.IsValid())
        {
            connection.Disconnect("Invalid account name or password."); //todo: use gameserverdisconnect
            return;
        }

        var foundedAccount = await UserBusinessRules.GetUser(account.Account, account.Password);

        if (foundedAccount == null)
        {
            connection.Disconnect("Account name or password is not correct.");
            return;
        }

        if (foundedAccount.UserInfo.BanishedAt is not null)
        {
            connection.Disconnect("Your account has been banished. Reason: " + foundedAccount.UserInfo.BanishmentReason);
            return;
        }

        connection.Send(new CharacterListPacket(foundedAccount, _serverConfiguration.ServerName,
            _serverConfiguration.ServerIp));
    }
}