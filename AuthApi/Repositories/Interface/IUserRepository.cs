using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IResponse> CreateUseAsync(UserReuestModel userReuestModel);
        Task<Response<List<UserResponseModel>>> GetAllUser();
    }
}
