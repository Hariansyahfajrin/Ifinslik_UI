namespace Data.Model
{
	public class NavMenuModel : BaseModel
	{
		public string? Name { get; set; }
		public string? URLMenu { get; set; }
		public string? ParentMenuID { get; set; }
		public string? Type { get; set; }
		public List<NavMenuModel> SubMenu { get; set; } = [];

	}
}