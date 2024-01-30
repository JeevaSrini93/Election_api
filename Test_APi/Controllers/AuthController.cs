using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_APi.Service;

namespace Test_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IVoterService _voterService;


        public AuthController(ITokenService tokenService, IVoterService voterService)
        {
            _tokenService = tokenService;
            _voterService= voterService;
        }

        [HttpPost("Voter/login")]
        public async Task<IActionResult> Login(string voterIdNumber, string password)
        {
            var voter = await _voterService.getSingle(voterIdNumber, password);
            if(voter == null) { return Unauthorized(); }
            // Add your authentication logic here, and if valid, generate a token
            var token = _tokenService.GenerateToken(voterIdNumber, "Voter");

            return Ok(new { Token = token });
        }

        [HttpPost("Ec/login")]
        public IActionResult Login2(string username, string password)
        {
            if (username == "Admin" && password == "Admin@123")
            {
                var token = _tokenService.GenerateToken(username, "ElectionCommissioner");
                return Ok(new { Token = token });
            }
            else
                return Unauthorized();
        }

    }
}
