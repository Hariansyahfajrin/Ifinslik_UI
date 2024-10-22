using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Helper.Auth
{

	public class AuthStateProvider : AuthenticationStateProvider, IDisposable
	{
		private readonly AuthService userService;

		public AuthStateProvider(AuthService userService)
		{
			this.userService = userService;
			AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
		}

		public Auth? CurrentUser { get; private set; }
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var principal = new ClaimsPrincipal();
			var user = await userService.FetchUserFromBrowserAsync();
			if (user is not null)
			{
				principal = user.ToClaimsPrincipal();
				CurrentUser = user;
			}

			return new(principal);
		}

		private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
		{
			var authenticationState = await task;

			if (authenticationState is not null)
			{
				CurrentUser = Auth.FromClaimsPrincipal(authenticationState.User);
			}
		}
		public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;


		public async Task LoginAsync(Auth user)
		{
			var principal = new ClaimsPrincipal();

			if (user is not null)
			{
				await userService.PersistUserToBrowserAsync(user);
				principal = user.ToClaimsPrincipal();
			}


			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
		}

		public async Task LogoutAsync()
		{
			await userService.ClearBrowserUserDataAsync();
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
		}


	}
}