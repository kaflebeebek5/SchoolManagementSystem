using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.Configurations;
using AuthApi.Services.Implementation;
using AuthApi.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.API.Controllers
{
    public class RoleController : BaseAPIController
    {
        private readonly IRoleService _RoleService;
        private readonly IMapper _mapper;


        public RoleController(IRoleService RoleService, IMapper mapper)
        {
            _RoleService = RoleService;

            _mapper = mapper;
        }

        [HttpPost("CreateRole")]
        [Authorize(Policy = "Role.C")]
        public async Task<IActionResult> CreateRole(RoleRequestModel RoleReuestModel)
        {
            var MappedData = _mapper.Map<Role>(RoleReuestModel);
            var Data = await _RoleService.CreateRoleAsync(MappedData);
            return Ok(Data);
        }
        [HttpGet("GetAll")]
        [Authorize(Policy = "Role.R")]
        public async Task<IActionResult> GetAllRole()
        {
            var Data = await _RoleService.GetAllRole();
            return Ok(Data);
        }
        [HttpGet("GetById")]
        [Authorize(Policy = "Role.R")]
        public async Task<IActionResult> GetRoleById(int Id)
        {
            var Data = await _RoleService.GetRoleById(Id);
            return Ok(Data);
        }
        [HttpGet("DeleteRole")]
        [Authorize(Policy = "Role.D")]
        public async Task<IActionResult> GetDeleteRole(int Id)
        {
            var Data = await _RoleService.DeleteRole(Id);
            return Ok(Data);
        }
        [HttpGet("GetUserRole")]
        [Authorize(Policy = "UR.R")]
        public async Task<IActionResult> GetUserRole(int UserId)
        {
            var Data = await _RoleService.GetRolebyUserIdAsync(UserId);
            return Ok(Data);
        }
        [HttpPost("UpdateUserRole")]
        [Authorize(Policy = "UR.U")]
        public async Task<IActionResult> UpdateUserRole(UserRoleRequestModel UserRoleRequest)
        {
            var Data = await _RoleService.UpdateUserRoleAsync(UserRoleRequest);
            return Ok(Data);
        }
    }
}
