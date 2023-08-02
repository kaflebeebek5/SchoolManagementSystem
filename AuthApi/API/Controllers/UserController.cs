using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.Configurations;
using AuthApi.Repositories;
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
      

        public UserController(IUserService userService,IMapper mapper)
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
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUser()
        {
            var Data=await _userService.GetAllUser();
            return Ok(Data);
        }
        [HttpGet("GetById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var Data = await _userService.GetUserById(Id);
            return Ok(Data);
        }
        [HttpGet("DeleteUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDeleteUser(int Id)
        {
            var Data = await _userService.DeleteUser(Id);
            return Ok(Data);
        }
    }
}
