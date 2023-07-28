using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IResponse> CreateUseAsync(User userReuestModel);
        Task<Response<List<UserResponseModel>>> GetAllUser();
    }
}
