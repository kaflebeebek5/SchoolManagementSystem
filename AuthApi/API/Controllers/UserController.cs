using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.Configurations;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.API.Controllers
{
    public class UserController : BaseAPIController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,IMapper mapper )
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("CreateUser")]
        [Authorize(Policy ="User.C")]
        public async Task<IActionResult> CreateUser(UserReuestModel userReuestModel)
        {
            var MappedData = _mapper.Map<User>(userReuestModel);
           var Data=await _userService.CreateUseAsync(MappedData);
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
