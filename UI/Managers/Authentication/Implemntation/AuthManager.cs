using Shared.Wrapper;
using UI.Managers.Authentication.Interface;
using UI.ViewModels;

namespace UI.Managers.Authentication.Implemntation
{
    public class AuthManager:IAuthManager
    {
        private readonly IConfiguration configuration;

        public AuthManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<IResponse> Login(AuthenticationViewModel viewModel)
        {
            return null;
        }
    }
}
