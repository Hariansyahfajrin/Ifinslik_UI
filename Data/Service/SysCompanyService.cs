using Data.Model;
using Helper;
using Helper.APIClient;
namespace Data.Service
{
	[Service]
	public class SysCompanyService
	{
		public readonly IFINSLIKClient _ifinslikClient;
		private readonly string _apiController = "SysCompany";
		private readonly string _routeGetRows = "GetRows";
		private readonly string _routeGetRowByID = "GetRowByID";
		private readonly string _routeInsert = "Insert";
		private readonly string _routeUpdateByID = "UpdateByID";
		private readonly string _routeDeleteByID = "DeleteByID";
		private readonly string _routeGetRowsForLookup = "GetRowsForLookup";


		public SysCompanyService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<SysCompanyModel>> GetRows(string keyword, int offset, int limit)
		{
			var res = await _ifinslikClient.GetRows<SysCompanyModel>(_apiController, _routeGetRows, new { keyword, offset, limit });
			return res.Data;
		}
		public async Task<SysCompanyModel> GetRowByID(string ID)
		{
			var res = await _ifinslikClient.GetRow<SysCompanyModel>(_apiController, _routeGetRowByID, ID);
			var data = res.Data;
			return data;
		}

		public async Task<BodyResponse<BaseModel>> Insert(SysCompanyModel model)
		{
			var res = await _ifinslikClient.Post(_apiController, _routeInsert, model);
			return res;
		}

		public async Task<BodyResponse<object>> UpdateByID(SysCompanyModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeUpdateByID, model);
			return res;
		}
		public async Task<BodyResponse<object>> DeleteByID(string[] ID)
		{
			var res = await _ifinslikClient.Delete(_apiController, _routeDeleteByID, ID);
			return res;
		}

		public async Task<List<SysCompanyModel>> GetRowsForLookup(string keyword, int offset, int limit)
		{
			var res = await _ifinslikClient.GetRows<SysCompanyModel>(_apiController, _routeGetRowsForLookup, new { keyword, offset, limit });
			return res.Data;
		}

	}
}