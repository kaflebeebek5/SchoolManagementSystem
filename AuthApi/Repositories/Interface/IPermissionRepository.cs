using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<IResponse> CreatePermissionAsync(Permission PermissionReuestModel);
        Task<Response<List<PermissionResponseModel>>> GetAllPermission();
        Task<Response<PermissionResponseModel>> GetPermissionById(int Id);
        Task<IResponse> DeletePermission(int Id);
        Task<IResponse> UpdatePermissionRoleAsync(PermissionRoleRequestModel PermissionRoles);
        Task<Response<List<PermissionRoleResponseModel>>> GetPermissionByRoleIdAsync(int RoleId);
    }
}
