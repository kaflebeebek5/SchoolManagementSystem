using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Configurations.Dapper;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Interface;
using Shared.Wrapper;

namespace AuthApi.Services.Implementation
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IResponse> CreateUseAsync(User userReuestModel)
        {
            return await _userRepository.CreateUseAsync(userReuestModel);
        }
        public async Task<Response<List<UserResponseModel>>> GetAllUser()
        {
            return await _userRepository.GetAllUser();
        }
    }
}
