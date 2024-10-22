using Data.Model;
using Helper;
using Helper.APIClient;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class D01HistoryService
	{
		// IFINSYS HTTP Client
		public readonly IFINSLIKClient _ifinslikClient;

		// API Controller
		public readonly string _apiController = "D01History";

		// API Route
		public readonly string _apiRouteGetRows = "GetRows";
		public readonly string _apiRouteGetRowByID = "GetRowByID";
		public readonly string _apiRouteInsert = "Insert";
		public readonly string _apiRouteGetUpdateByID = "UpdateByID";
		public readonly string _apiRouteGetDeleteByID = "DeleteByID";
		private readonly string _routeGetRowsForLookup = "GetRowsForLookup";

		// Constructor
		public D01HistoryService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<D01HistoryModel>?> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			// var parameters = new
			// {
			// 	Keyword = keyword,
			// 	Offset = offset,
			// 	Limit = limit
			// };

			// Get List Data
			var res = await _ifinslikClient.GetRows<D01HistoryModel>(_apiController, _apiRouteGetRows, new { Keyword = keyword, Offset = offset, Limit = limit, FormTransactionHistoryID = formTransactionHistoryID });

			return res?.Data;
		}
		public async Task<D01HistoryModel?> GetRowByID(string? id)
		{
			// Deklarasi parameters
			var parameters = new
			{
				ID = id
			};

			// Get Single Data
			var res = await _ifinslikClient.GetRow<D01HistoryModel>(_apiController, _apiRouteGetRowByID, parameters);

			return res?.Data;
		}
		public async Task<BodyResponse<BaseModel>> Insert(D01HistoryModel model)
		{
			// Mengirim model ke API Insert dengan HTTP Post
			var res = await _ifinslikClient.Post(_apiController, _apiRouteInsert, model);
			return res;
		}
		public async Task<BodyResponse<object>> UpdateByID(D01HistoryModel model)
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
		public async Task<List<D01HistoryModel>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId)
		{
			var res = await _ifinslikClient.GetRows<D01HistoryModel>(_apiController, _routeGetRowsForLookup, new { keyword, offset, limit, formTransactionId });
			return res.Data;
		}


	}
}