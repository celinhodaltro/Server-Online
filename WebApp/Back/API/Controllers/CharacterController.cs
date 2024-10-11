using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessRules;
using Server.Entities;
using Server.Util;


namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(UserManager<ApplicationUser> userManager, 
                                  SignInManager<ApplicationUser> signManager, 
                                  IConfiguration configuration, 
                                  UserBusinessRules logBusinessRules,
                                  CharacterBusinessRules playerBusinessRules) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await playerBusinessRules.GetAll());
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetById([FromRoute] int playerId)
        {
            return Response(await playerBusinessRules.GetById(playerId));
        }

        [HttpGet("User/{userUniqueId}")]
        public async Task<IActionResult> GetByUserUniqueId([FromRoute] Guid userUniqueId)
        {
            var characters = await playerBusinessRules.GetByUserUniqueId(userUniqueId);
            return Ok(characters);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Character player)
        {
            await playerBusinessRules.Create(player);
            return Ok();
        }

    }
}
