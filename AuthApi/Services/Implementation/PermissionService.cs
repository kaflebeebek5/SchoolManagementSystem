using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Interface;
using Shared.Wrapper;

namespace AuthApi.Services.Implementation
{
    public class PermissionService:IPermissionService
    {
        private readonly IPermissionRepository _PermissionRepository;

        public PermissionService(IPermissionRepository PermissionRepository)
        {
            _PermissionRepository = PermissionRepository;
        }
        public async Task<IResponse> CreatePermissionAsync(Permission PermissionReuestModel)
        {
            return await _PermissionRepository.CreatePermissionAsync(PermissionReuestModel);
        }
        public async Task<Response<List<PermissionResponseModel>>> GetAllPermission()
        {
            return await _PermissionRepository.GetAllPermission();
        }
        public async Task<Response<PermissionResponseModel>> GetPermissionById(int Id)
        {
            return await _PermissionRepository.GetPermissionById(Id);
        }
        public async Task<IResponse> DeletePermission(int Id)
        {
            return await _PermissionRepository.DeletePermission(Id);
        }
        public async Task<Response<List<PermissionRoleResponseModel>>> GetPermissionByRoleIdAsync(int RoleId)
        {
            return await _PermissionRepository.GetPermissionByRoleIdAsync(RoleId);
        }
        public async Task<IResponse> UpdatePermissionRoleAsync(PermissionRoleRequestModel PermissionRoles)
        {
            return await _PermissionRepository.UpdatePermissionRoleAsync(PermissionRoles);
        }
    }

}
