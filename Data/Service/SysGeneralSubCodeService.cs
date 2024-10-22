using Data.Model;
using Helper;
using Helper.APIClient;
namespace Data.Service
{
	[Service]
	public class SysGeneralSubCodeService
	{
		public readonly IFINSLIKClient _ifinslikClient;
		private readonly string _apiController = "SysGeneralSubCode";
		private readonly string _routeGetRows = "GetRows";
		private readonly string _routeGetRowByID = "GetRowByID";
		private readonly string _routeGetRowByCode = "GetRowByCode";
		private readonly string _routeInsert = "Insert";
		private readonly string _routeUpdateByID = "UpdateByID";
		private readonly string _routeDeleteByID = "DeleteByID";
		private readonly string _routeChangeStatus = "ChangeStatus";
		private readonly string _routeGetRowsForLookup = "GetRowsForLookup";

		public SysGeneralSubCodeService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<SysGeneralSubCodeModel>> GetRows(string keyword, int offset, int limit, string? SysGeneralCodeID)
		{
			var res = await _ifinslikClient.GetRows<SysGeneralSubCodeModel>(_apiController, _routeGetRows, new { keyword, offset, limit, SysGeneralCodeID });
			return res.Data;
		}
		public async Task<SysGeneralSubCodeModel> GetRowByID(string? ID)
		{
			var res = await _ifinslikClient.GetRow<SysGeneralSubCodeModel>(_apiController, _routeGetRowByID, ID);
			var data = res.Data;
			return data;
		}
		public async Task<SysGeneralSubCodeModel?> GetRowByCode(string? Code)
		{
			var res = await _ifinslikClient.GetRow<SysGeneralSubCodeModel>(_apiController, _routeGetRowByCode, new { Code });
			var data = res?.Data;
			return data;
		}

		public async Task<BodyResponse<BaseModel>> Insert(SysGeneralSubCodeModel model)
		{
			var res = await _ifinslikClient.Post(_apiController, _routeInsert, model);
			return res;
		}

		public async Task<BodyResponse<object>> UpdateByID(SysGeneralSubCodeModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeUpdateByID, model);
			return res;
		}
		public async Task<BodyResponse<object>> DeleteByID(string[] ID)
		{
			var res = await _ifinslikClient.Delete(_apiController, _routeDeleteByID, ID);
			return res;
		}
		public async Task<BodyResponse<object>> ChangeStatus(SysGeneralSubCodeModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeChangeStatus, model);
			return res;
		}
		public async Task<List<SysGeneralSubCodeModel>> GetRowsForLookup(string keyword, int offset, int limit, string? code)
		{
			var res = await _ifinslikClient.GetRows<SysGeneralSubCodeModel>(_apiController, _routeGetRowsForLookup, new { keyword, offset, limit, code });
			return res.Data;
		}


	}
}