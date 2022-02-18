using JOIEnergy.Domain;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IJWTTokenManagerService _jwtTokenService;

        public LoginController(IConfiguration config, IJWTTokenManagerService jwtTokenService)
        {
            _config = config;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var tokenString = _jwtTokenService.GenerateToken(login.UserId, login.Password);
            response = Ok(new { token = tokenString });

            return response;
        }
    }
}
