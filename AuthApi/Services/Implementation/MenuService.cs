using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Interface;
using Shared.Wrapper;

namespace AuthApi.Services.Implementation
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _MenuRepository;

        public MenuService(IMenuRepository MenuRepository)
        {
            _MenuRepository = MenuRepository;
        }
        public async Task<IResponse> CreateMenuAsync(Menu MenuReuestModel)
        {
            return await _MenuRepository.CreateMenuAsync(MenuReuestModel);
        }
        public async Task<Response<List<MenuResponseModel>>> GetAllMenu()
        {
            return await _MenuRepository.GetAllMenu();
        }
        public async Task<Response<MenuResponseModel>> GetMenuById(int Id)
        {
            return await _MenuRepository.GetMenuById(Id);
        }
        public async Task<IResponse> DeleteMenu(int Id)
        {
            return await _MenuRepository.DeleteMenu(Id);
        }
        public async Task<Response<List<MenuRoleResponseModel>>> GetMenuByRoleIdAsync(int RoleId)
        {
            return await _MenuRepository.GetMenuByRoleIdAsync(RoleId);
        }
        public async Task<IResponse> UpdateMenuRoleAsync(MenuRoleRequestModel menuRoles)
        {
            return await _MenuRepository.UpdateMenuRoleAsync(menuRoles);
        }

    }
}
