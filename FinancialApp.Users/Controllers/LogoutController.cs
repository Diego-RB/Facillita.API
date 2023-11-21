using Facillita.Users.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Facillita.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult LogoutUser()
        {
            Result result = _logoutService.LogoutUser();
            if (result.IsFailed)
                return Unauthorized();

            return Ok(result.Successes);
        }
    }
}
