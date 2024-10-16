using Networking.Handlers.ClientVersion;
using Networking.Packets.Incoming;
using Networking.Packets.Outgoing.Login;
using Server.BusinessRules;
using Server.Common.Contracts.Network;
using Server.Configurations;
using System;
using System.Security.Principal;

namespace Networking.Handlers.LogIn;

public class AccountLoginHandler : PacketHandler
{
    private readonly ClientProtocolVersion ClientProtocolVersion;
    private readonly UserBusinessRules UserBusinessRules;
    private readonly ServerConfiguration ServerConfiguration;

    public AccountLoginHandler(ServerConfiguration serverConfiguration,
                               ClientProtocolVersion clientProtocolVersion, 
                               UserBusinessRules UserBusinessRules)
    {
        this.UserBusinessRules = UserBusinessRules;
        this.ServerConfiguration = serverConfiguration;
        this.ClientProtocolVersion = clientProtocolVersion;
    }

    public override async void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var account = new AccountLoginPacket(message);

        if (!ClientProtocolVersion.IsSupported(account.ProtocolVersion))
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

        if (string.IsNullOrWhiteSpace(account.Account))
        {
            connection.Disconnect("Account is Empty!"); 
            return;
        }
        else if (string.IsNullOrWhiteSpace(account.Password))
        {
            connection.Disconnect("Password is Empty!");
            return;
        }

        var foundedAccount = await UserBusinessRules.GetUser(account.Account, account.Password);

        if (foundedAccount is null)
        {
            connection.Disconnect("Account name or password is not correct.");
            return;
        }

        if (foundedAccount.UserInfo.BanishedAt is not null)
        {
            connection.Disconnect("Your account has been banished. Reason: " + foundedAccount.UserInfo.BanishmentReason);
            return;
        }

        if(foundedAccount.UserInfo.Players is null || foundedAccount.UserInfo.Players.Count == 0)
        {
            connection.Disconnect("Your account not have Characters.");
            return;
        }

        connection.Send(new CharacterListPacket(foundedAccount, ServerConfiguration.ServerName,
            ServerConfiguration.ServerIp));
    }
}