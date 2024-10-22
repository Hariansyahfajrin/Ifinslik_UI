using Helper.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Config
{
	public static class AuthExtensions
	{
		public static void AddAuthScoped(this IServiceCollection services)
		{
			services.AddScoped<AuthService>();
			services.AddScoped<AuthStateProvider>();
			services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());
			services.AddScoped<RoleAccessManager>();
			services.AddCascadingAuthenticationState();
		}
	}
}