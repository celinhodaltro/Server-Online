using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessRules;
using Server.Entities;

namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController(UserManager<ApplicationUser> userManager, 
                                  SignInManager<ApplicationUser> signManager, 
                                  IConfiguration configuration, 
                                  AccountBusinessRules logBusinessRules,
                                  PlayerBusinessRules playerBusinessRules) : BaseController
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Player player)
        {
            await playerBusinessRules.Create(player);
            return Ok();
        }

    }
}
