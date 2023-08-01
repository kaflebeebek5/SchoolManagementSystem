using AuthApi.API.DbModel;
using AuthApi.Authentication.Model;
using AuthApi.Configurations.Dapper;
using AuthApi.DbContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Shared.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Authentication
{
    public class AuthService : BaseService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly SchoolManagementDbContext _db;
        private readonly IMapper _mapper;
        //private IEncryptionService _encryptionService;
        public const string JWT_SECURITY_KEY = "ASDFGHJKLqtfaaftfztfzljkjmkjhugyftyftdxrfxxthdtryjtrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrcccccccccccdxdd";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger, SchoolManagementDbContext schoolManagementDbContext, IMapper mapper /*IEncryptionService encryptionService*/) : base(configuration, logger)
        {
            _configuration = configuration;
            _logger = logger;
            _db = schoolManagementDbContext;
            _mapper = mapper;
            //_encryptionService = encryptionService;
        }
        public async Task<IResponse<LoginResponse>> GetLoginToken(LoginRequest authenticationRequest)
        {
            var User = _db.Users.Where(x => x.Username == authenticationRequest.UserName).FirstOrDefault();

            ////CHECK USERNAME EXIST ON USER TABLE OR  NOT////
            if (User == null)
            {
                return await Response<LoginResponse>.FailAsync("Invalid Username or Password.");
            }
            if (User.Password != authenticationRequest.Password)
            {
                return await Response<LoginResponse>.FailAsync("Invalid Username or Password.");
            }
            var UserClaim = _mapper.Map<UserClaimModel>(User);
            var Roles = _db.userRoles.Where(x => x.UserID == User.UserId).Select(x => x.RoleId).ToList();
            var PermissionClaim = _db.Permissions.Join(_db.PermissionRoles, c => c.PermissionId, s => s.PermissionId, (c, s) => new { c, s })
                    .Select(x => new
                    {
                        x.c.PermissionType,
                        x.c.PermissionValue
                    }).ToList();
            List<PermissionClaimModel> permissionClaimModels = new();
            foreach(var items in  PermissionClaim)
            {
                permissionClaimModels.Add(new PermissionClaimModel
                {
                    PermissionType=items.PermissionType!,
                    PermissionValue=items.PermissionValue!
                });
            }
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            LoginResponse loginResponse = new LoginResponse
            {
                UserName = User.Username!,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken=CreateToken(UserClaim, permissionClaimModels)
            };
            return await Response<LoginResponse>.SuccessAsync(loginResponse);
        }
        private string CreateToken(UserClaimModel userAccount, List<PermissionClaimModel> roleClaims)
        {
            var permissionClaims = new List<Claim>();
            foreach (var claim in roleClaims)
            {
                permissionClaims.Add(new Claim(claim.PermissionType, claim.PermissionValue));
            }

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, userAccount.UserName),
                    new(ClaimTypes.Email, userAccount.Email),
                    new(ClaimTypes.Name, userAccount.Name?? string.Empty),
                    new(ClaimTypes.Role, userAccount.Role),
                    new(ClaimTypes.MobilePhone, userAccount.MobileNo ?? string.Empty),
                    new("UserId", userAccount.UserId.ToString()),
                    new("DepartmentId", userAccount.DepartmentId.ToString()),
                    new("SecurityStamp",Guid.NewGuid().ToString()),
                    new("DepartmentName",userAccount.Department)
                }
            .Union(permissionClaims);
            var signingCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:TokenSecret"]!)),
                   SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken(
                   claims: claims,
                   expires: DateTime.UtcNow.AddDays(2),
                   signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;

         
        }
    }
}
