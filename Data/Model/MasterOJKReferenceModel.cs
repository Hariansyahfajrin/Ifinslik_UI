namespace Data.Model
{
    public class MasterOJKReferenceModel : BaseModel
    {
        public string? Code { get; set; }
        public string? ReferenceTypeID { get; set; }
        public string? Description { get; set; }
        public string? OJKCode { get; set; }
        public int? IsActive { get; set; }
        public SysGeneralSubCodeModel? SysGeneralSubCodeModel { get; set; }

    }
}