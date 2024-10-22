using Config;
using Helper.Auth;
using Radzen;

namespace IFinancing360_UI.Shared
{
  partial class Login
  {
    string userName = "admin";
    string password = "admin";
    protected Auth auth = new();

    async void OnLogin(LoginArgs args, string name)
    {
      Loading.Show();
      await AuthStateProvider.LoginAsync(new Auth { UserName = args.Username, Password = args.Password });

      Loading.Close();
      NavigationManager.NavigateTo(AppConfig.BASE_PATH);
    }

  }
}