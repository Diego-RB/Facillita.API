using FinancialApp.Users.Data.Requests;
using FinancialApp.Users.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogUser(LoginRequest request)
        {
            Result result = _loginService.LogUser(request);
            if (result.IsFailed)
                return Unauthorized(result.Errors.FirstOrDefault());

            return Ok(result.Successes.FirstOrDefault());
        }
    }
}
