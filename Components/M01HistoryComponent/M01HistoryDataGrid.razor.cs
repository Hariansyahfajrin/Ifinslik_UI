using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.M01HistoryComponent
{
	public partial class M01HistoryDataGrid
	{
		#region Service
		[Inject] M01HistoryService M01HistoryService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionHistoryID { get; set; }
		#endregion

		#region Component Field
		DataGrid<M01HistoryModel> dataGrid = null!;
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
		protected async Task<List<M01HistoryModel>?> LoadData(string keyword)
		{
			return await M01HistoryService.GetRows(keyword, 0, 100, FormTransactionHistoryID ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/inquiry/datapengurushistory/{FormTransactionHistoryID}/pengurushistory01/add");
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

				await M01HistoryService.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
