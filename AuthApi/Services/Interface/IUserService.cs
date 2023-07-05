using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using Shared.Wrapper;

namespace AuthApi.Services.Interface
{
    public interface IUserService
    {
        Task<IResponse> CreateUseAsync(UserReuestModel userReuestModel);
        Task<Response<List<UserResponseModel>>> GetAllUser();
    }
}
