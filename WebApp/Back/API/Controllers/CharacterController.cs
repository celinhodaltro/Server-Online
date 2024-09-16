using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessRules;
using Server.Entities;

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

        [HttpPost]
        public async Task<IActionResult> GetByUserUniqueId([FromBody] Guid userUniqueId)
        {
            return Response(await playerBusinessRules.GetByUserUniqueId(userUniqueId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Player player)
        {
            await playerBusinessRules.Create(player);
            return Ok();
        }

    }
}
