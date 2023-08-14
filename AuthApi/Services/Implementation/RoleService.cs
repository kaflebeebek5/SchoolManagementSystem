using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Interface;
using Shared.Wrapper;

namespace AuthApi.Services.Implementation
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _RoleRepository;

        public RoleService(IRoleRepository RoleRepository)
        {
            _RoleRepository = RoleRepository;
        }
        public async Task<IResponse> CreateRoleAsync(Role RoleReuestModel)
        {
            return await _RoleRepository.CreateRoleAsync(RoleReuestModel);
        }
        public async Task<Response<List<RoleResponseModel>>> GetAllRole()
        {
            return await _RoleRepository.GetAllRole();
        }
        public async Task<Response<RoleResponseModel>> GetRoleById(int Id)
        {
            return await _RoleRepository.GetRoleById(Id);
        }
        public async Task<IResponse> DeleteRole(int Id)
        {
            return await _RoleRepository.DeleteRole(Id);
        }
        public async Task<Response<List<UserRoleResponseModel>>> GetRolebyUserIdAsync(int UserId)
        {
            return await _RoleRepository.GetRolebyUserIdAsync(UserId);
        }
        public async Task<IResponse> UpdateUserRoleAsync(UserRoleRequestModel requestModel)
        {
            return await _RoleRepository.UpdateUserRoleAsync(requestModel);
        }
    }
}
