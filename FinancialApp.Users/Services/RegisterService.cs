using AutoMapper;
using Facillita.Users.Data.Dtos;
using Facillita.Users.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace Facillita.Users.Services
{
    public class RegisterService
    {
        private IMapper _mapper; 
        private UserManager<IdentityUser<int>> _userManager;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result RegisterUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
            var resultIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);
            if (resultIdentity.Result.Succeeded)
            {
                var addRole = _userManager.AddToRoleAsync(userIdentity, "regular");
                if (addRole.Result.Succeeded)
                {
                    return Result.Ok();
                }
            }

            return Result.Fail("Register failed");
        }
    }
}
