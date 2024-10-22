using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.P01Component
{
	public partial class P01DataGrid
	{
		#region Service
		[Inject] P01Service P01Service { get; set; } = null!;
		[Inject] FormTransactionService FormTransactionService { get; set; }
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; }
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		#region Component Field
		DataGrid<P01Model> dataGrid = null!;
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
		protected async Task<List<P01Model>?> LoadData(string keyword)
		{
			return await P01Service.GetRows(keyword, 0, 100, FormTransactionID ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/datapenjamin/{FormTransactionID}/Penjamin02/add");
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

				await P01Service.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
