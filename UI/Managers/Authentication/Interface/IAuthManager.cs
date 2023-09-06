using Shared.Wrapper;
using UI.ViewModels;

namespace UI.Managers.Authentication.Interface
{
    public interface IAuthManager:IManager
    {
         Task<IResponse> Login(AuthenticationViewModel viewModel);
    }
}
