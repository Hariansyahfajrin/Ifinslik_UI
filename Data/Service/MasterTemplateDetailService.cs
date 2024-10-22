using Data.Model;
using Helper;
using Helper.APIClient;
namespace Data.Service
{
	[Service]
	public class MasterTemplateDetailService
	{
		public readonly IFINSLIKClient _ifinslikClient;
		private readonly string _apiController = "MasterTemplateDetail";
		private readonly string _routeGetRows = "GetRows";
		private readonly string _routeGetRowByID = "GetRowByID";
		private readonly string _routeInsert = "Insert";
		private readonly string _routeUpdateByID = "UpdateByID";
		private readonly string _routeDeleteByID = "DeleteByID";
		private readonly string _routeChangeStatus = "ChangeStatus";

		public MasterTemplateDetailService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<MasterTemplateDetailModel>> GetRows(string keyword, int offset, int limit, string? MasterTemplateID)
		{
			var res = await _ifinslikClient.GetRows<MasterTemplateDetailModel>(_apiController, _routeGetRows, new { keyword, offset, limit, MasterTemplateID });
			return res.Data;
		}
		public async Task<MasterTemplateDetailModel> GetRowByID(string? ID)
		{
			var res = await _ifinslikClient.GetRow<MasterTemplateDetailModel>(_apiController, _routeGetRowByID, ID);
			var data = res.Data;
			return data;
		}

		public async Task<BodyResponse<BaseModel>> Insert(MasterTemplateDetailModel model)
		{
			var res = await _ifinslikClient.Post(_apiController, _routeInsert, model);
			return res;
		}

		public async Task<BodyResponse<object>> UpdateByID(MasterTemplateDetailModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeUpdateByID, model);
			return res;
		}
		public async Task<BodyResponse<object>> DeleteByID(string[] ID)
		{
			var res = await _ifinslikClient.Delete(_apiController, _routeDeleteByID, ID);
			return res;
		}
		public async Task<BodyResponse<object>> ChangeStatus(MasterTemplateDetailModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeChangeStatus, model);
			return res;
		}

	}
}