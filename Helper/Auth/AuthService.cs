using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Helper.Auth
{
	public class AuthService(ProtectedSessionStorage protectedSessionStorage)
	{
		private readonly string _storageKey = "identity";

		public async Task PersistUserToBrowserAsync(Auth user)
		{
			string userJson = JsonSerializer.Serialize(user);
			await protectedSessionStorage.SetAsync(_storageKey, userJson);
		}

		public async Task<Auth?> FetchUserFromBrowserAsync()
		{
			try
			{
				var storedUserResult = await protectedSessionStorage.GetAsync<string>(_storageKey);

				if (storedUserResult.Success && !string.IsNullOrEmpty(storedUserResult.Value))
				{
					var user = JsonSerializer.Deserialize<Auth>(storedUserResult.Value);
					return user;
				}
			}
			catch (InvalidOperationException)
			{
			}

			return null;
		}

		public async Task ClearBrowserUserDataAsync() => await protectedSessionStorage.DeleteAsync(_storageKey);

	}
}