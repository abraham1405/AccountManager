using AccountManager.Application.DTOs;
using AccountManager.Application.Services;
using AccountManager.Domain.Entities;
using AccountManager.Request;
using AccountManager.Shared.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace AccountManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(AccountService accountService, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountDataRequest request)
        {
            if (request == null)
            {
                return BadRequest("Poor user response");
            }
            var requestDto = mapper.Map<RegisterDto>(request);
            bool isValidate = await VerifyMail.IsExistMailAsync(request.Email);

            if (!isValidate)
                return BadRequest("The email is not valid or the domain does not exist.");

            var registerEntity = mapper.Map<Account>(requestDto);
            bool result = await accountService.RegisterAsync(registerEntity);
            if (result == false)
                return Conflict("An account with this email or username already exists.");

            return CreatedAtAction(
                nameof(Register),
                new { email = registerEntity.Email },
                new { message = "Account successfully created", email = registerEntity.Email }
            );
        }
    }
}
