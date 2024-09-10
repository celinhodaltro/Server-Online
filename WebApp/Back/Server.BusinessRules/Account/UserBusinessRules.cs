using Server.Entities;
using System.Provider;
using Microsoft.AspNetCore.Identity;

namespace Server.BusinessRules;

public class UserBusinessRules
{

    public DefaultProvider DefaultProvider { get; set; }
    public SignInManager<ApplicationUser> SignManager { get; set; }
    public UserBusinessRules(DefaultProvider defaultProvider, SignInManager<ApplicationUser> signManager)
    {
        DefaultProvider = defaultProvider;
        SignManager = signManager;
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

    public async Task<User?> GetUser(string? name, string? password)
    {

        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(password))
            throw new Exception("Name or Password is empty");

        var Users = await DefaultProvider.GetAllAsync<User>();
        return Users.FirstOrDefault(x => x.Email.Equals(name) && x.Password.Equals(password));

    }

}

