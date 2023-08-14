using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Services.Interface
{
    public interface IPermissionService
    {
        Task<IResponse> CreatePermissionAsync(Permission PermissionReuestModel);
        Task<Response<List<PermissionResponseModel>>> GetAllPermission();
        Task<Response<PermissionResponseModel>> GetPermissionById(int Id);
        Task<IResponse> DeletePermission(int Id);
        Task<Response<List<PermissionRoleResponseModel>>> GetPermissionByRoleIdAsync(int RoleId);
        Task<IResponse> UpdatePermissionRoleAsync(PermissionRoleRequestModel PermissionRoles);
    }
}
