using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.Configurations;
using AuthApi.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.API.Controllers
{
    public class MenuController : BaseAPIController
    {
        private readonly IMenuService _MenuService;
        private readonly IMapper _mapper;


        public MenuController(IMenuService MenuService, IMapper mapper)
        {
            _MenuService = MenuService;

            _mapper = mapper;
        }

        [HttpPost("CreateMenu")]
        [Authorize(Policy = "Menu.C")]
        public async Task<IActionResult> CreateMenu(MenuRequestModel MenuReuestModel)
        {
            var MappedData = _mapper.Map<Menu>(MenuReuestModel);
            var Data = await _MenuService.CreateMenuAsync(MappedData);
            return Ok(Data);
        }
        [HttpGet("GetAll")]
        [Authorize(Policy = "Menu.R")]
        public async Task<IActionResult> GetAllMenu()
        {
            var Data = await _MenuService.GetAllMenu();
            return Ok(Data);
        }
        [HttpGet("GetById")]
        [Authorize(Policy = "Menu.R")]
        public async Task<IActionResult> GetMenuById(int Id)
        {
            var Data = await _MenuService.GetMenuById(Id);
            return Ok(Data);
        }
        [HttpGet("DeleteMenu")]
        [Authorize(Policy = "Menu.D")]
        public async Task<IActionResult> GetDeleteMenu(int Id)
        {
            var Data = await _MenuService.DeleteMenu(Id);
            return Ok(Data);
        }
        [HttpGet("GetMenuRole")]
        [Authorize(Policy = "MR.R")]
        public async Task<IActionResult>GetMenuRole(int RoleId)
        {
            var Data=await _MenuService.GetMenuByRoleIdAsync(RoleId);
            return Ok(Data);
        }
        [HttpPost("UpdateMenuRole")]
        [Authorize(Policy = "MR.U")]
        public async Task<IActionResult> UpdateMenuRole(MenuRoleRequestModel menuRoleRequest)
        {
            var Data = await _MenuService.UpdateMenuRoleAsync(menuRoleRequest);
            return Ok(Data);
        }
    }
}
