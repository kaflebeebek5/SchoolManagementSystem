using Microsoft.AspNetCore.Components;
using System.Reflection;
using UI.Managers.Authentication.Interface;
using UI.ViewModels;

namespace UI.Pages.Authentication
{
    public partial class Login
    {
        [Inject] public IAuthManager authManager { get; set; } = default!;
        private AuthenticationViewModel model=new();
        private bool loading=false;

        private async void OnValidSubmit()
        {
            loading = true;
            var data = await authManager.Login(model);
            if(data.Succeeded)
            {

            }
        }
    }
}
