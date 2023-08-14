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
    public class PermissionController : BaseAPIController
    {
        private readonly IPermissionService _PermissionService;
        private readonly IMapper _mapper;
        public PermissionController(IPermissionService PermissionService, IMapper mapper)
        {
            _PermissionService = PermissionService;

            _mapper = mapper;
        }

        [HttpPost("CreatePermission")]
        [Authorize(Policy = "PER.C")]
        public async Task<IActionResult> CreatePermission(PermissionRequestModel PermissionReuestModel)
        {
            var MappedData = _mapper.Map<Permission>(PermissionReuestModel);
            var Data = await _PermissionService.CreatePermissionAsync(MappedData);
            return Ok(Data);
        }
        [HttpGet("GetAll")]
        [Authorize(Policy = "PER.R")]
        public async Task<IActionResult> GetAllPermission()
        {
            var Data = await _PermissionService.GetAllPermission();
            return Ok(Data);
        }
        [HttpGet("GetById")]
        [Authorize(Policy = "PER.R")]
        public async Task<IActionResult> GetPermissionById(int Id)
        {
            var Data = await _PermissionService.GetPermissionById(Id);
            return Ok(Data);
        }
        [HttpGet("DeletePermission")]
        [Authorize(Policy = "PER.D")]
        public async Task<IActionResult> GetDeletePermission(int Id)
        {
            var Data = await _PermissionService.DeletePermission(Id);
            return Ok(Data);
        }
        [HttpGet("GetPermissionRole")]
        [Authorize(Policy = "PR.R")]
        public async Task<IActionResult> GetPermissionRole(int RoleId)
        {
            var Data = await _PermissionService.GetPermissionByRoleIdAsync(RoleId);
            return Ok(Data);
        }
        [HttpPost("UpdatePermissionRole")]
        [Authorize(Policy = "PR.U")]
        public async Task<IActionResult> UpdatePermissionRole(PermissionRoleRequestModel PermissionRoleRequest)
        {
            var Data = await _PermissionService.UpdatePermissionRoleAsync(PermissionRoleRequest);
            return Ok(Data);
        }
    }
}
