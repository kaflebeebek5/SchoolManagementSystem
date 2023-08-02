using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Configurations.Dapper;
using AuthApi.DbContext;
using AuthApi.Repositories.Interface;
using Shared.Wrapper;

namespace AuthApi.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;
        private readonly SchoolManagementDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger, SchoolManagementDbContext schoolManagementDbContext, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _logger = logger;
            _db = schoolManagementDbContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<IResponse> CreateUseAsync(User userReuestModel)
        {
            try
            {
                userReuestModel.CreatedDate = System.DateTime.Now;
                _unitOfWork.Add(userReuestModel);
                await _unitOfWork.CompleteAsync();
                return await Response.SuccessAsync("User Saved Successfully!");
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }
        public async Task<Response<List<User>>> GetAllUser()
        {
            var Data = await _unitOfWork.GetAllAsync<User>();
            return await Response<List<User>>.SuccessAsync(Data);

        }
        public async Task<Response<User>> GetUserById(int Id)
        {
            var Data = await _unitOfWork.GetByIdAsync<User>(Id);
            return await Response<User>.SuccessAsync(Data);
        }
        public async Task<IResponse> DeleteUser(int Id)
        {
            try
            {
                User user = new();
                user = _db.Users.Where(x=>x.UserId==Id).FirstOrDefault()!;
                if (user != null)
                    _unitOfWork.Delete<User>(user);
                await _unitOfWork.CompleteAsync();
                return await Response.SuccessAsync("User Deleted Successfully!");
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }
    }
}
