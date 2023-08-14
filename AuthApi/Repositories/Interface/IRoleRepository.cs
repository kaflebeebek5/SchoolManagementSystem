using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IResponse> CreateRoleAsync(Role userReuestModel);
        Task<Response<List<RoleResponseModel>>> GetAllRole();
        Task<Response<RoleResponseModel>> GetRoleById(int Id);
        Task<IResponse> DeleteRole(int Id);
        Task<Response<List<UserRoleResponseModel>>> GetRolebyUserIdAsync(int UserId);
        Task<IResponse> UpdateUserRoleAsync(UserRoleRequestModel requestModel);
    }
}
