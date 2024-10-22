using System.Security.Claims;

namespace Helper.Auth
{
	public class Auth
	{
		public string? UserName { get; set; }
		public string? Password { get; set; }

		public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
		{
				new (ClaimTypes.Name, UserName ?? throw new InvalidOperationException()),
				new (ClaimTypes.Hash, Password ?? throw new InvalidOperationException()),
		}, "BlazorSchool"));


		public static Auth FromClaimsPrincipal(ClaimsPrincipal principal) => new()
		{
			UserName = principal.FindFirstValue(ClaimTypes.Name),
			Password = principal.FindFirstValue(ClaimTypes.Hash)
		};

		public string FullName { get; set; } = "";


	}
}