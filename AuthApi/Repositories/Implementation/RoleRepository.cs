using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.API.ResponseModel;
using AuthApi.Configurations.Dapper;
using AuthApi.DbContext;
using AuthApi.Repositories.Interface;
using AutoMapper;
using Shared.Wrapper;
using System.Text.Json;

namespace AuthApi.Repositories.Implementation
{
    public class RoleRepository : BaseService,IRoleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleRepository> _logger;
        private readonly SchoolManagementDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public RoleRepository(IConfiguration configuration, ILogger<RoleRepository> logger, SchoolManagementDbContext schoolManagementDbContext, IUnitOfWork unitOfWork, IMapper mapper): base(configuration, logger)
        {
            _configuration = configuration;
            _logger = logger;
            _db = schoolManagementDbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResponse> CreateRoleAsync(Role RoleReuestModel)
        {
            RoleReuestModel.CreatedDate = System.DateTime.Now;
            _unitOfWork.Add(RoleReuestModel);
            await _unitOfWork.CompleteAsync();
            return await Response.SuccessAsync("Role Saved Successfully!");
        }
        public async Task<Response<List<RoleResponseModel>>> GetAllRole()
        {
            var Data = await _unitOfWork.GetAllAsync<Role>();
            var Roles = _mapper.Map<List<RoleResponseModel>>(Data);
            return await Response<List<RoleResponseModel>>.SuccessAsync(Roles);

        }
        public async Task<Response<RoleResponseModel>> GetRoleById(int Id)
        {
            var Data = await _unitOfWork.GetByIdAsync<Role>(Id);
            var Role = _mapper.Map<RoleResponseModel>(Data);
            return await Response<RoleResponseModel>.SuccessAsync(Role);
        }
        public async Task<IResponse> DeleteRole(int Id)
        {
            Role Role = new();
            Role = _db.Roles.Where(x => x.RoleId == Id).FirstOrDefault()!;
            if (Role != null)
                _unitOfWork.Delete<Role>(Role);
            await _unitOfWork.CompleteAsync();
            return await Response.SuccessAsync("Role Deleted Successfully!");
        }
        public async Task<IResponse> UpdateUserRoleAsync(UserRoleRequestModel requestModel)
        {
            var JsonData = JsonSerializer.Serialize(requestModel.userRoles);
            var Message = await this.ExecuteAsync("Execute Auth.spAuthSetup @Flag='UpdateUserRole',@JsonUserRole=@JsonUserRole,@UserId=@UserId",
                new
                {
                    JsonUserRole = JsonData,
                    requestModel.UserId
                });
            return await Response<IResponse>.SuccessAsync(Message.ToString());
        }
        public async Task<Response<List<UserRoleResponseModel>>> GetRolebyUserIdAsync(int UserId)
        {
            var Data = await this.GetQueryResultAsync<UserRoleResponseModel>("Execute Auth.spAuthSetup @Flag='GetUserRole',@UserId=@UserId",
              new
              {
                  UserId
              });
            return await Response<List<UserRoleResponseModel>>.SuccessAsync(Data);

        }
    }
}
