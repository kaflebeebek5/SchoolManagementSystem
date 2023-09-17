using Shared.Wrapper;
using UI.Managers.Authentication.Interface;
using UI.ViewModels;

namespace UI.Managers.Authentication.Implemntation
{
    public class AuthManager : IAuthManager
    {
        HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5) // Set a longer timeout, e.g., 5 minutes
        };
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthManager(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<IResponse> Login(AuthenticationViewModel viewModel)
        {
            var Data = await _httpClient.GetAsync("Auth/Login");
            return null;
        }
    }
}
