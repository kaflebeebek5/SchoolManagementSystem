using AuthApi.API.RequestModel;
using AuthApi.Configurations;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.API.Controllers
{
    public class UserController : BaseAPIController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserReuestModel userReuestModel)
        {
           var Data=await _userService.CreateUseAsync(userReuestModel);
            return Ok(Data);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUser()
        {
            var Data=await _userService.GetAllUser();
            return Ok(Data);
        }
    }
}
