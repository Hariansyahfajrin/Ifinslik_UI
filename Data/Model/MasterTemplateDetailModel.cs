namespace Data.Model
{
    public class MasterTemplateDetailModel : BaseModel
    {
        public string? MasterTemplateID { get; set; }
        public string? TemplateCode { get; set; }
        public string? PatchPeriode { get; set; }
        public string? FormatType { get; set; }
        public int? OrderRow { get; set; }
        public string? Description { get; set; }
        public string? FieldName { get; set; }
        public string? RefferenceTypeCode { get; set; }
        public int? IsFix { get; set; }
        public int? FixLength { get; set; }
        public string? FixLengthFiller { get; set; }
        public string? FixLengthFillerPosition { get; set; }
        public int? IsActive { get; set; }

        #region SysGeneralSubCodeModel
        public string? SysGeneralSubCodeFormatType { get; set; }
        public string? SysGeneralSubCodeRefferenceTypeCode { get; set; }
        #endregion

    }
    // public class RoleAccess
    // {
    // 	public string? Code {get; set;}
    //     public string? Description {get; set;}
    //     public int? OrderKey {get; set;}
    //     public int? IsActive {get; set;}
    // }

    // public enum FinanceCompanyType
    // {
    // 	SYR = 'A',
    // 	KVN = 'B',

    // }


}