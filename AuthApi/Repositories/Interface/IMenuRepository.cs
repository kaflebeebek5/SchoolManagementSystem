using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Repositories.Interface
{
    public interface IMenuRepository
    {
        Task<IResponse> CreateMenuAsync(Menu userReuestModel);
        Task<Response<List<MenuResponseModel>>> GetAllMenu();
        Task<Response<MenuResponseModel>> GetMenuById(int Id);
        Task<IResponse> DeleteMenu(int Id);
        Task<Response<List<MenuRoleResponseModel>>> GetMenuByRoleIdAsync(int RoleId);
        Task<IResponse> UpdateMenuRoleAsync(MenuRoleRequestModel menuRoles);
    }
}
