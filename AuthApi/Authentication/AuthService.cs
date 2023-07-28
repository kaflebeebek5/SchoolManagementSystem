using AuthApi.Configurations.Dapper;

namespace AuthApi.Authentication
{
    public class AuthService:BaseService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        //private IEncryptionService _encryptionService;
        public const string JWT_SECURITY_KEY = "as#$@$dxeteXfaere433dfsfd3432GJHJH#$@";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger /*IEncryptionService encryptionService*/) : base(configuration, logger)
        {
            _configuration = configuration;
            _logger = logger;
            //_encryptionService = encryptionService;
        }
    }
}
