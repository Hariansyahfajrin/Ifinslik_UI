using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.M01Component
{
	public partial class M01DataGrid
	{
		#region Service
		[Inject] M01Service M01Service { get; set; } = null!;
		[Inject] FormTransactionService FormTransactionService { get; set; }
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; }
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		#region Component Field
		DataGrid<M01Model> dataGrid = null!;
		#endregion

		#region Field

		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		protected async Task<List<M01Model>?> LoadData(string keyword)
		{
			return await M01Service.GetRows(keyword, 0, 100, FormTransactionID ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/datapengurus/{FormTransactionID}/pengurus01/add");
		}
		#endregion

		#region Delete
		private async void Delete()
		{
			var selectedData = dataGrid.selectedData;

			if (!selectedData.Any())
			{
				await NoDataSelectedAlert();
				return;
			}

			bool? result = await Confirm();

			if (result == true)
			{
				Loading.Show();

				await M01Service.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
