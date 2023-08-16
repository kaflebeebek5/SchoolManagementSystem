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
    public class PermissionRepository : BaseService, IPermissionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionRepository> _logger;
        private readonly SchoolManagementDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionRepository(IConfiguration configuration, ILogger<PermissionRepository> logger, SchoolManagementDbContext schoolManagementDbContext, IUnitOfWork unitOfWork, IMapper mapper) : base(configuration, logger)
        {
            _configuration = configuration;
            _logger = logger;
            _db = schoolManagementDbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResponse> CreatePermissionAsync(Permission PermissionReuestModel)
        {
            _unitOfWork.Add(PermissionReuestModel);
            await _unitOfWork.CompleteAsync();
            return await Response.SuccessAsync("Permission Saved Successfully!");
        }
        public async Task<Response<List<PermissionResponseModel>>> GetAllPermission()
        {
            var Data = await _unitOfWork.GetAllAsync<Permission>();
            var Permissions = _mapper.Map<List<PermissionResponseModel>>(Data);
            return await Response<List<PermissionResponseModel>>.SuccessAsync(Permissions);

        }
        public async Task<Response<PermissionResponseModel>> GetPermissionById(int Id)
        {
            var Data = await _unitOfWork.GetByIdAsync<Permission>(Id);
            var Permission = _mapper.Map<PermissionResponseModel>(Data);
            return await Response<PermissionResponseModel>.SuccessAsync(Permission);
        }
        public async Task<IResponse> DeletePermission(int Id)
        {
            Permission Permission = new();
            Permission = _db.Permissions.Where(x => x.PermissionId == Id).FirstOrDefault()!;
            if (Permission != null)
                _unitOfWork.Delete<Permission>(Permission);
            await _unitOfWork.CompleteAsync();
            return await Response.SuccessAsync("Permission Deleted Successfully!");
        }
        public async Task<IResponse> UpdatePermissionRoleAsync(PermissionRoleRequestModel PermissionRoles)
        {
            var JsonData = JsonSerializer.Serialize(PermissionRoles.permissionViewModels);
            var Data = await this.ExecuteAsync("Execute Auth.spAuthSetup @Flag='UpdatePermissionRole',@JsonPermissionRole=@PermissionJsonData,@RoleID=@RoleId,@MenuId=@MenuId",
                new
                {
                    PermissionJsonData = JsonData,
                    PermissionRoles.RoleId,
                    PermissionRoles.MenuId
                });
            return await Response<IResponse>.SuccessAsync(Data.ToString());
        }
        public async Task<Response<List<PermissionRoleResponseModel>>> GetPermissionByRoleIdAsync(int RoleId)
        {
            var Data = await this.GetQueryResultAsync<PermissionRoleResponseModel>("Execute Auth.spAuthSetup @Flag='GetPermissionRole',@RoleID=@RoleId",
              new
              {
                  RoleId
              });
            return await Response<List<PermissionRoleResponseModel>>.SuccessAsync(Data);
        }
    }
}
