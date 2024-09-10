using Microsoft.AspNetCore.Identity;
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
                                IConfiguration configuration, 
                                LogBusinessRules logBusinessRules,
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
                    await userBusinessRules.CreateUserInfo(userInfo.Id);
                    var result = await this.Login(userInfo);
                    return result;
                }
                else
                    return BadRequest(resultRegister.Errors.Select(Error => Error.Description));
            }
            catch (Exception ex)
            {
                await logBusinessRules.CreateLog(new LogTrack { Level = LogLevelEnum.Error, Message = "Error! Login", Details = ex.Message });
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
                    return userBusinessRules.BuildToken(userInfo);
                else
                    return BadRequest("User or Password Invalid!");

            }
            catch (Exception ex)
            {
                await logBusinessRules.CreateLog(new LogTrack { Level = LogLevelEnum.Error, Message = "Error! Login", Details = ex.Message });
                throw;
            }

        }



    }
}
