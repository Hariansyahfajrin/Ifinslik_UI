
namespace Data.Model
{
    public class TxtStatusOJKModel : BaseModel
    {
        public string? Code { get; set; }
        public string? CompanyCode { get; set; }
        public string? PeriodeDate { get; set; }
        public string? FinanceCompanyType { get; set; }
        public DateTime? Date { get; set; }

        #region SysGeneralSubCodeModel
        public string? SysGeneralSubCodeFinanceCompanyType { get; set; }
        #endregion

        #region BuktiBerhasilLapor
        public string? BuktiBerhasilLaporPeriodePelaporan { get; set; }
        #endregion

    }
}