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
    public class PlayerController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, 
                                IConfiguration configuration, AccountBusinessRules logBusinessRules) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _playerService.GetAll());
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetById([FromRoute] int playerId)
        {
            return Response(await _playerService.GetById(playerId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlayerPostRequest player)
        {
            await _playerService.Create(player);
            return Ok();
        }

    }
}
