using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.F01HistoryComponent
{
	public partial class F01HistoryDataGrid
	{
		#region Service
		[Inject] F01HistoryService F01HistoryService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionHistoryID { get; set; }
		#endregion

		#region Component Field
		DataGrid<F01HistoryModel> dataGrid = null!;
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
		protected async Task<List<F01HistoryModel>?> LoadData(string keyword)
		{
			return await F01HistoryService.GetRows(keyword, 0, 100, FormTransactionHistoryID ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/inquiry/datakredithistory/{FormTransactionHistoryID}/datakredithistory01/add");
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

				await F01HistoryService.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
