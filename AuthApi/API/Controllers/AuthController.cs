using AuthApi.Authentication;
using AuthApi.Authentication.Model;
using AuthApi.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.API.Controllers
{
    public class AuthController : BaseAPIController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var Data=await _authService.GetLoginToken(loginRequest);
            return Ok(Data);
        }
    }
}
