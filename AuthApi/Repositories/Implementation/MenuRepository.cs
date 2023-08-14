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
    public class MenuRepository : BaseService, IMenuRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuRepository> _logger;
        private readonly SchoolManagementDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public MenuRepository(IConfiguration configuration, ILogger<MenuRepository> logger, SchoolManagementDbContext schoolManagementDbContext, IUnitOfWork unitOfWork, IMapper mapper) : base(configuration, logger)
        {
            _configuration = configuration;
            _logger = logger;
            _db = schoolManagementDbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResponse> CreateMenuAsync(Menu MenuReuestModel)
        {
            MenuReuestModel.CreatedDate = System.DateTime.Now;
            _unitOfWork.Add(MenuReuestModel);
            await _unitOfWork.CompleteAsync();
            return await Response.SuccessAsync("Menu Saved Successfully!");
        }
        public async Task<Response<List<MenuResponseModel>>> GetAllMenu()
        {
            var Data = await _unitOfWork.GetAllAsync<Menu>();
            var Menus = _mapper.Map<List<MenuResponseModel>>(Data);
            return await Response<List<MenuResponseModel>>.SuccessAsync(Menus);

        }
        public async Task<Response<MenuResponseModel>> GetMenuById(int Id)
        {
            var Data = await _unitOfWork.GetByIdAsync<Menu>(Id);
            var Menu = _mapper.Map<MenuResponseModel>(Data);
            return await Response<MenuResponseModel>.SuccessAsync(Menu);
        }
        public async Task<IResponse> DeleteMenu(int Id)
        {
            Menu Menu = new();
            Menu = _db.Menuss.Where(x => x.MenuID == Id).FirstOrDefault()!;
            if (Menu != null)
                _unitOfWork.Delete<Menu>(Menu);
            await _unitOfWork.CompleteAsync();
            return await Response.SuccessAsync("Menu Deleted Successfully!");
        }
        public async Task<IResponse> UpdateMenuRoleAsync(MenuRoleRequestModel menuRoles)
        {
            var JsonData = JsonSerializer.Serialize(menuRoles.menuRoles);
            var Data = await this.ExecuteAsync("Execute Auth.spAuthSetup @Flag='UpdateMenuRole',@MenuJsonData=@MenuJsonData,@RoleID=@RoleId,@ParentId=@ParentId",
                new
                {
                    MenuJsonData = JsonData,
                    menuRoles.RoleId,
                    menuRoles.MenuId
                });
            return await Response.SuccessAsync(Data.ToString());
        }
        public async Task<Response<List<MenuRoleResponseModel>>> GetMenuByRoleIdAsync(int RoleId)
        {
            var Data = await this.GetQueryResultAsync<MenuRoleResponseModel>("Execute Auth.spAuthSetup @Flag='GetMenuRole',@RoleID=@RoleId",
                new
                {
                    RoleId
                });
            return await Response<List<MenuRoleResponseModel>>.SuccessAsync(Data);
        }
    }
}
