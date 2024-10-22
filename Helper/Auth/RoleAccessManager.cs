using Config;
using Data.Model;
using Helper.APIClient;

namespace Helper.Auth
{
	public class RoleAccessModel
	{
		public string? Code { get; set; }
		public string? Name { get; set; }
		public string? MenuURL { get; set; }
		public string? RoleAccess { get; set; }
	}

	public class RoleAccessManager(IFINSYSClient ifinsysClient)
	{
		private List<RoleAccessModel>? _roles;

		private async Task<List<RoleAccessModel>> FetchRoleAccess()
		{
			return await ifinsysClient.GetRows<RoleAccessModel>("SysMenuRole", "GetRowsForRoleAccess", new { ModuleCode = AppConfig.MODULE }).ContinueWith((res) => _roles = res.Result?.Data ?? []);

		}

		public async Task<List<RoleAccessModel>> GetRoles()
		{
			if (_roles is null)
			{
				return await FetchRoleAccess();
			}
			return _roles;
		}

		public async Task<bool> HasAccess(string Code)
		{
			var roles = await GetRoles();
			return roles.Any(x => x.Code == Code);
		}
	}
}