using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.BuktiBerhasilLaporComponent
{
	public partial class BuktiBerhasilLaporDataGrid
	{
		#region Service
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; } = null!;
		#endregion

		#region Component Field
		DataGrid<BuktiBerhasilLaporModel> dataGrid = null!;
		#endregion

		#region Field
		public BuktiBerhasilLaporModel filter = new()
		{
			Status = "ALL"
		};

		public Dictionary<string, string> statusDdl = new(){
						{"ALL", "ALL"},
						{"HOLD", "HOLD"},
						{"SUCCESS", "SUCCESS"},
						{"ON PROCESS", "ON PROCESS"},
						{"REPORTING", "REPORTING"},
						{"DONE", "DONE"},
				};
		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		protected async Task<List<BuktiBerhasilLaporModel>?> LoadData(string keyword)
		{
			return await BuktiBerhasilLaporService.GetRows(keyword, 0, 100, filter.Status);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/periodepelaporanojk/add");
		}
		#endregion
	}
}
