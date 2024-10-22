using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.FormTransactionHistoryComponent
{
	public partial class FormTransactionHistoryDataGrid
	{
		#region Service
		[Inject] FormTransactionHistoryService FormTransactionHistoryService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormType { get; set; }
		[Parameter, EditorRequired] public string? Link { get; set; }
		#endregion

		#region Component Field
		DataGrid<FormTransactionHistoryModel> dataGrid = null!;
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
		protected async Task<List<FormTransactionHistoryModel>?> LoadData(string keyword)
		{
			return await FormTransactionHistoryService.GetRows(keyword, 0, 100, FormType ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/inquiry/{Link}/add");
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

				await FormTransactionHistoryService.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
