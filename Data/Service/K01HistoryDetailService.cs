using Data.Model;
using Helper;
using Helper.APIClient;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class K01HistoryService
	{
		// IFINSYS HTTP Client
		public readonly IFINSLIKClient _ifinslikClient;

		// API Controller
		public readonly string _apiController = "K01History";

		// API Route
		public readonly string _apiRouteGetRows = "GetRows";
		public readonly string _apiRouteGetRowByID = "GetRowByID";
		public readonly string _apiRouteInsert = "Insert";
		public readonly string _apiRouteGetUpdateByID = "UpdateByID";
		public readonly string _apiRouteGetDeleteByID = "DeleteByID";

		// Constructor
		public K01HistoryService(IFINSLIKClient ifinslikClient)
		{
			_ifinslikClient = ifinslikClient;
		}

		public async Task<List<K01HistoryModel>?> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			// var parameters = new
			// {
			// 	Keyword = keyword,
			// 	Offset = offset,
			// 	Limit = limit
			// };

			// Get List Data
			var res = await _ifinslikClient.GetRows<K01HistoryModel>(_apiController, _apiRouteGetRows, new { Keyword = keyword, Offset = offset, Limit = limit, FormTransactionHistoryID = formTransactionHistoryID });

			return res?.Data;
		}
		public async Task<K01HistoryModel?> GetRowByID(string? id)
		{
			// Deklarasi parameters
			var parameters = new
			{
				ID = id
			};

			// Get Single Data
			var res = await _ifinslikClient.GetRow<K01HistoryModel>(_apiController, _apiRouteGetRowByID, parameters);

			return res?.Data;
		}
		public async Task<BodyResponse<BaseModel>> Insert(K01HistoryModel model)
		{
			// Mengirim model ke API Insert dengan HTTP Post
			var res = await _ifinslikClient.Post(_apiController, _apiRouteInsert, model);
			return res;
		}
		public async Task<BodyResponse<object>> UpdateByID(K01HistoryModel model)
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

	}
}