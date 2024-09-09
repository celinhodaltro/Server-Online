using Server.Entities;
using System.Provider;

namespace Server.BusinessRules;

public class AccountBusinessRules
{

    public DefaultProvider DefaultProvider { get; set; }

    public AccountBusinessRules(DefaultProvider defaultProvider)
    {
        DefaultProvider = defaultProvider;
    }

    public async Task CreateUserInfo(int UserId)
    {

        if (UserId is 0 or <0)
            throw new Exception("User is Null");

        var UserInfo = new UserInfo 
        {
            PremiumTime = 0,
            Secret = "",
            UserId = UserId
        };

        await DefaultProvider.CreateAsync(UserInfo);
    }
}

