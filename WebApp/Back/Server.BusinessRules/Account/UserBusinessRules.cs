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
    public SignInManager<ApplicationUser> SignManager { get; set; }
    public LogBusinessRules LogBusinessRules { get; set; }
    public IConfiguration Configuration { get; set; }

    public UserBusinessRules(DefaultProvider defaultProvider, SignInManager<ApplicationUser> signManager, LogBusinessRules logBusinessRules, IConfiguration configuration)
    {
        DefaultProvider = defaultProvider;
        SignManager = signManager;
        LogBusinessRules = logBusinessRules;
        Configuration = configuration;
    }

    public async Task CreateUserInfo(int UserId)
    {

        if (UserId is 0 or < 0)
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

    public UserToken BuildToken(User userinfo)
    {
        Claim[] claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.UniqueName, userinfo.Email),
                new Claim("AppMain", "Teste.com"),
                new Claim(JwtRegisteredClaimNames.Aud, Configuration["Jwt:Audience"]),
                new Claim(JwtRegisteredClaimNames.Iss, Configuration["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        DateTime expiration = DateTime.UtcNow.AddHours(2);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }

}

