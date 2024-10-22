
namespace Data.Model
{
    public class FormTransactionModel : BaseModel
    {
        public string? Code { get; set; }
        public string? CompanyCode { get; set; }
        public string? FinanceCompanyType { get; set; }
        public string? FormType { get; set; }
        public DateTime? Date { get; set; }
        public string? PeriodePelaporan { get; set; }

        #region SysGeneralSubCodeModel
        public string? SysGeneralSubCodeFormType { get; set; }
        public string? SysGeneralSubCodeFinanceCompanyType { get; set; }
        #endregion


    }

}
