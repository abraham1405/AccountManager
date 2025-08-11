using AccountManager.Application.DTOs;
using AccountManager.Application.Services;
using AccountManager.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(AuthService authService, IMapper mapper) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //var sessionCookie = Request.Cookies["Auth_Login"];
            //if (sessionCookie != null)
            //{

            //}
            if (request == null)
                return BadRequest("this request is empty :(");
            var resquestDto = mapper.Map<LoginDto>(request);
            var result = await authService.Login(resquestDto);

            if (result == null)
                return Unauthorized("Incorrect credentials");

            Response.Cookies.Append("Auth_Login", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(5)
            });

            return CreatedAtAction(nameof(Login),
                new { token = result },
                new { message = "Login successful", token = result });

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var sessionCookie = Request.Cookies["Auth_Login"];

            if (string.IsNullOrEmpty(sessionCookie))
                return BadRequest("No active session found");

            var checkSession = await authService.checkSession(sessionCookie);

            if (checkSession == null)
                return NotFound("Session not Found");

            await authService.Logout(checkSession);
            
            Response.Cookies.Delete("Auth_Login");

            return Accepted(new { message = "Logout successful" });
        }
    }
}
