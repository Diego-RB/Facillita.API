using Facillita.API.Data.Dtos.User;
using Facillita.API.Interfaces.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Facillita.API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            UserDto readDto = _userService.AddUser(userDto);

            //If there's already an income with same description in the same month, it'll return null from AddIncome method
            if (readDto != null)
                return Ok();

            return BadRequest($"User already exists in {CultureInfo.GetCultureInfo("pt-Br").DateTimeFormat.GetMonthName(DateTime.Now.Month)}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string uid)
        {
            Result result = _userService.DeleteUser(uid);
            if (result.IsFailed)
                return NotFound();

            return NoContent();
        }





    }
}
