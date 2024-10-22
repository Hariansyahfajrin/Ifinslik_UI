using Data.Model;
using Helper;
using Helper.APIClient;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysGlobalParamService
	{
		// IFINSYS HTTP Client
		public readonly IFINSLIKClient _ifinslikClient;

		// API Controller
		public readonly string _apiController = "SysGlobalParam";

		// API Route
		public readonly string _apiRouteGetRows = "GetRows";
		public readonly string _apiRouteGetRowByID = "GetRowByID";
		public readonly string _apiRouteInsert = "Insert";
		public readonly string _apiRouteGetUpdateByID = "UpdateByID";
		public readonly string _apiRouteGetDeleteByID = "DeleteByID";
		private readonly string _routeChangeEditableStatus = "ChangeEditableStatus";

		// Constructor
		public SysGlobalParamService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<SysGlobalParamModel>?> GetRows(string keyword, int offset, int limit)
		{
			// Deklarasi parameters
			var parameters = new
			{
				Keyword = keyword,
				Offset = offset,
				Limit = limit
			};

			// Get List Data
			var res = await _ifinslikClient.GetRows<SysGlobalParamModel>(_apiController, _apiRouteGetRows, parameters);

			return res?.Data;
		}
		public async Task<SysGlobalParamModel?> GetRowByID(string? id)
		{
			// Deklarasi parameters
			var parameters = new
			{
				ID = id
			};

			// Get Single Data
			var res = await _ifinslikClient.GetRow<SysGlobalParamModel>(_apiController, _apiRouteGetRowByID, parameters);

			return res?.Data;
		}
		public async Task<BodyResponse<BaseModel>> Insert(SysGlobalParamModel model)
		{
			// Mengirim model ke API Insert dengan HTTP Post
			var res = await _ifinslikClient.Post(_apiController, _apiRouteInsert, model);
			return res;
		}
		public async Task<BodyResponse<object>> UpdateByID(SysGlobalParamModel model)
		{
			// Mengirim model ke API Update dengan HTTP Put
			var res = await _ifinslikClient.Put(_apiController, _apiRouteGetUpdateByID, model);
			return res;
		}
		public async Task<BodyResponse<object>> DeleteByID(string[] id)
		{
			// Mengirim model ke API Update dengan HTTP Delete
			var res = await _ifinslikClient.Delete(_apiController, _apiRouteGetDeleteByID, id);
			return res;
		}
		public async Task<BodyResponse<object>> ChangeEditableStatus(SysGlobalParamModel model)
		{
			var res = await _ifinslikClient.Put(_apiController, _routeChangeEditableStatus, model);
			return res;
		}


	}
}