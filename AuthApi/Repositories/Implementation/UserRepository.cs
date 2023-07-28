using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Configurations.Dapper;
using AuthApi.DbContext;
using AuthApi.Repositories.Interface;
using Shared.Wrapper;

namespace AuthApi.Repositories.Implementation
{
    public class UserRepository:BaseService,IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;
        private readonly SchoolManagementDbContext _db;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger,SchoolManagementDbContext schoolManagementDbContext):base(configuration,logger) 
        {
            _configuration = configuration;
            _logger = logger;
            _db = schoolManagementDbContext;
        }
        public async Task<IResponse> CreateUseAsync(User userReuestModel)
        {
            try
            {
                userReuestModel.CreatedDate=System.DateTime.Now;
                _db.Users.Add(userReuestModel);
                _db.SaveChanges();
                return await Response.SuccessAsync("User Saved Successfully!");
            }
            catch (Exception ex)
            {

                return await Response.FailAsync(ex.Message);
            }
            //var Data =await  this.ExecuteAsync("Execute Procedure spUser @Flag='I',@Name=@Name,@Email=@Email,@Username=@Username,@Address=@Address,@ContactNo=@ContactNo," +
            //    "@JoinDate=@JoinDate,@ValidFrom=@ValidFrom,@ValidTo=@ValidTo,@Lock=@Lock,@Photo=@Photo,@Status=@Status",
            //    new
            //    {
            //        Name= userReuestModel.Name,
            //        Email= userReuestModel.Email,
            //        Username= userReuestModel.Username,
            //        Address= userReuestModel.Address,
            //        ContactNo= userReuestModel.ContactNo,
            //        JoinDate= userReuestModel.JoinDate,
            //        ValidFrom= userReuestModel.ValidFrom,
            //        ValidTo= userReuestModel.ValidTo,
            //        Lock= userReuestModel.Lock,
            //        Photo= userReuestModel.Photo,
            //        Status= userReuestModel.Status
            //    });
            //  return await Response.SuccessAsync(Data.ToString());

        }
        public async Task<Response<List<UserResponseModel>>>GetAllUser()
        {
            var Data = await this.GetQueryResultAsync<UserResponseModel>("Select * From Auth.tblUser");
            return await Response<List<UserResponseModel>>.SuccessAsync(Data);
        }
    }
}
