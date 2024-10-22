using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.F01Component
{
	public partial class F01DataGrid
	{
		#region Service
		[Inject] F01Service F01Service { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		#region Component Field
		DataGrid<F01Model> dataGrid = null!;
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
		protected async Task<List<F01Model>?> LoadData(string keyword)
		{
			return await F01Service.GetRows(keyword, 0, 100, FormTransactionID);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/datakredit/{FormTransactionID}/datakredit01/add");
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

				await F01Service.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
