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
        public async Task<Response<List<User>>> GetAllUser()
        {
            return await _userRepository.GetAllUser();
        }
        public async Task<Response<User>> GetUserById(int Id)
        {
            return await _userRepository.GetUserById(Id);
        }
        public async Task<IResponse> DeleteUser(int Id)
        {
            return await _userRepository.DeleteUser(Id);
        }
    }
}
