using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Services.Interface
{
    public interface IUserService
    {
        Task<IResponse> CreateUseAsync(User userReuestModel);
        Task<Response<List<User>>> GetAllUser();
        Task<Response<User>> GetUserById(int Id);
    }
}
