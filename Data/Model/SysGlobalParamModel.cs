namespace Data.Model
{
	public class SysGlobalParamModel : BaseModel
	{
		public string? Code { get; set; }
		public string? Description { get; set; }
		public string? Value { get; set; }
		public int? IsEditable { get; set; }

	}
}