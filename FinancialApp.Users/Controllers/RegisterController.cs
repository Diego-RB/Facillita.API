using FinancialApp.Users.Data.Dtos;
using FinancialApp.Users.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult RegisterUser(CreateUserDto createDto)
        {
            Result result =  _registerService.RegisterUser(createDto);
            if (result.IsFailed)
                return StatusCode(500);

            return Ok();
        }
    }
}
