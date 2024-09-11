using Server.Entities;
using System.Provider;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Server.BusinessRules;

public class UserBusinessRules
{

    public DefaultProvider DefaultProvider { get; set; }
    public LogBusinessRules LogBusinessRules { get; set; }

    public UserBusinessRules(DefaultProvider defaultProvider, LogBusinessRules logBusinessRules)
    {
        DefaultProvider = defaultProvider;
        LogBusinessRules = logBusinessRules;
    }

    public async Task CreateUserInfo(ApplicationUser UserApp)
    {

        if (Guid.Parse(UserApp.Id) == Guid.Empty)
            throw new Exception("User is Null");


        var User = new User { 
            Email = UserApp.Email,
            Password = UserApp.PasswordHash,
            UserType = 0,
            UniqueId = Guid.Parse(UserApp.Id)
        };

        User = await DefaultProvider.CreateAsync(User);


        var UserInfo = new UserInfo
        {
            UniqueId = User.UniqueId,
            UserId = User.Id
        };

        await DefaultProvider.CreateAsync(UserInfo);
    }

    public async Task<User?> GetUser(string? name, string? password)
    {
        try
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(password))
                throw new Exception("Name or Password is empty");

            var Users = await DefaultProvider.GetAllAsync<User>();
            return Users.FirstOrDefault(x => x.Email.Equals(name) && x.Password.Equals(password));

        }
        catch (Exception ex)
        {
            await LogBusinessRules.CreateLog(new LogTrack (LogLevelEnum.Error, ex.ToString(), ex.Message ));
            throw;
        }
    }


}

