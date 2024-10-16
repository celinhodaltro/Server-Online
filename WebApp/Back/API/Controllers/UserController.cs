﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.BusinessRules;
using Server.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserManager<ApplicationUser> userManager, 
                                SignInManager<ApplicationUser> signManager, 
                                IConfiguration Configuration, 
                                LoggerBusinessRules logBusinessRules,
                                UserBusinessRules userBusinessRules) : Controller
    {

        [HttpPost("Register")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] User userInfo)
        {
            try
            {
                var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };

                var resultRegister = await userManager.CreateAsync(user, userInfo.Password);


                if (resultRegister.Succeeded)
                {
                    await userBusinessRules.CreateUserInfo(userInfo, user);
                    var result = await this.Login(userInfo);
                    return result;
                }
                else
                    return BadRequest(resultRegister.Errors.Select(Error => Error.Description));
            }
            catch (Exception ex)
            {
                await logBusinessRules.CreateLog(new LogTrack(LogLevelEnum.Error, "Error! Login", ex.Message));
                throw;
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] User userInfo)
        {

            try
            {
                var result = await signManager.PasswordSignInAsync(userInfo?.Email, userInfo?.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var UserResult = await userBusinessRules.GetUser(userInfo.Email, userInfo.Password);
                    return BuildToken(UserResult);
                }
                else
                    return BadRequest("User or Password Invalid!");

            }
            catch (Exception ex)
            {
                await logBusinessRules.CreateLog(new LogTrack (LogLevelEnum.Error, "Error! Login", ex.Message));
                throw;
            }

        }

        [HttpGet("GetUser/{UserUniqueId}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] Guid UserUniqueId)
        {

            try
            {
                var result = await userBusinessRules.GetUser(UserUniqueId);
                return result;

            }
            catch (Exception ex)
            {
                await logBusinessRules.CreateLog(new LogTrack(LogLevelEnum.Error, "Error! Login", ex.Message));
                throw;
            }

        }

        private UserToken BuildToken(User userinfo)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userinfo.Email),
                new Claim("AppMain", "Teste.com"),
                new Claim("UserId", userinfo.UniqueId.ToString()),
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
}
