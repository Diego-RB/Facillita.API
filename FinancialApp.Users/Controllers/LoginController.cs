using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Facillita.Users.Data.Requests;
using Facillita.Users.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Facillita.Users.Controllers
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
