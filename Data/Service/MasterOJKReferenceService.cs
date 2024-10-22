using Data.Model;
using Helper;
using Helper.APIClient;
namespace Data.Service
{
	[Service]
	public class MasterOJKReferenceService
	{
		public readonly IFINSLIKClient _ifinslikClient;
		private readonly string _apiController = "MasterOJKReference";
		private readonly string _routeGetRows = "GetRows";
		private readonly string _routeGetRowByID = "GetRowByID";
		private readonly string _routeInsert = "Insert";
		private readonly string _routeUpdateByID = "UpdateByID";
		private readonly string _routeDeleteByID = "DeleteByID";
		private readonly string _routeChangeStatus = "ChangeStatus";
		private readonly string _routeGetRowsForLookup = "GetRowsForLookup";


		public MasterOJKReferenceService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<MasterOJKReferenceModel>> GetRows(string keyword, int offset, int limit, string? SysGeneralSubCodeID)
		{
			var res = await _ifinslikClient.GetRows<MasterOJKReferenceModel>(_apiController, _routeGetRows, new { keyword, offset, limit, SysGeneralSubCodeID });
			return res.Data;
		}
		public async Task<MasterOJKReferenceModel> GetRowByID(string? ID)
		{
			var res = await _ifinslikClient.GetRow<MasterOJKReferenceModel>(_apiController, _routeGetRowByID, ID);
			var data = res.Data;
			return data;
		}

		public async Task<BodyResponse<BaseModel>> Insert(MasterOJKReferenceModel model)
		{
			var res = await _ifinslikClient.Post(_apiController, _routeInsert, model);
			return res;
		}

		public async Task<BodyResponse<object>> UpdateByID(MasterOJKReferenceModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeUpdateByID, model);
			return res;
		}
		public async Task<BodyResponse<object>> DeleteByID(string[] ID)
		{
			var res = await _ifinslikClient.Delete(_apiController, _routeDeleteByID, ID);
			return res;
		}
		public async Task<BodyResponse<object>> ChangeStatus(MasterOJKReferenceModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeChangeStatus, model);
			return res;
		}
		public async Task<List<MasterOJKReferenceModel>> GetRowsForLookup(string keyword, int offset, int limit, string? code)
		{
			var res = await _ifinslikClient.GetRows<MasterOJKReferenceModel>(_apiController, _routeGetRowsForLookup, new { keyword, offset, limit, code });
			return res.Data;
		}

	}
}