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


    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountApiService _accountApiService;

        public AccountController(IAccountApiService accountApiService)
        {
            _accountApiService = accountApiService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountPostRequest request)
        {
            await _accountApiService.Create(request);
            return Ok();
        }
    }
}
