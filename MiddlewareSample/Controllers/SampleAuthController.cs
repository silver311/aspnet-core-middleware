using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiddlewareSample.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SampleAuthController : ControllerBase
    {
        [HttpGet("secured")]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("This is a secured endpoint");
        }

        [HttpPost("authenticate/admin")]
        public IActionResult AuthenticateAdmin()
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, "User"),
                new("Admin", "true")
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync("CookieAuth", principal);

            return Ok("User authenticated.");
        }

        [HttpGet("secured/admin")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAdmin()
        {
            return Ok("This is a secured admin endpoint");
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate()
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, "User")
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return Ok("User authenticated.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");

            return Ok("User logged out.");
        }

        [HttpGet("public")]
        public IActionResult GetPublic()
        {
            return Ok("This is a public endpoint");
        }
    }
}
