using Data.Model;
using Helper;
using Helper.APIClient;

namespace Data.Service
{
	[Service]
	public class NavMenuService
	{
		private IFINSYSClient _ifinsysClient { get; }
		private readonly string _controller = "SysMenu";
		public NavMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		public async Task<List<NavMenuModel>> GetAllMenu(string moduleCode)
		{
			var res = await _ifinsysClient.GetRows<NavMenuModel>(_controller, "GetRowsForMenu", new { moduleCode });

			var menus = res?.Data;
			var groupped = menus?.GroupJoin(menus, parent => parent.ID, child => child.ParentMenuID, (parent, children) => new NavMenuModel
			{
				ID = parent.ID,
				URLMenu = parent.URLMenu,
				Name = parent.Name,
				ParentMenuID = parent.ParentMenuID,
				Type = parent.Type,
				SubMenu = children.ToList()
			}).Where(m => string.IsNullOrEmpty(m.ParentMenuID)).ToList();

			return groupped ?? [];
		}
	}
}