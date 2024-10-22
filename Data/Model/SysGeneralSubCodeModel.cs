namespace Data.Model
{
	public class SysGeneralSubCodeModel : BaseModel
	{
		public string? SysGeneralCodeID { get; set; }
		public string? Code { get; set; }
		public string? Description { get; set; }
		public int? OrderKey { get; set; }
		public int? IsActive { get; set; }

		#region SysGeneralCodeModel
		public string? SysGeneralCodeDescription { get; set; }
		public int? SysGeneralCodeIsEditable { get; set; }
		#endregion

	}
}