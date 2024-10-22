using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.F02Component
{
	public partial class F02DataGrid
	{
		#region Service
		[Inject] F02Service F02Service { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		[Parameter, EditorRequired] public string? ID { get; set; }

		#endregion

		#region Component Field
		DataGrid<F02Model> dataGrid = null!;
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
		protected async Task<List<F02Model>?> LoadData(string keyword)
		{
			return await F02Service.GetRows(keyword, 0, 100, FormTransactionID ?? "");
		}
		#endregion

		#region Add
		private void Back()
		{
			NavigationManager.NavigateTo("/transaction/datakreditjoin");
		}
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/datakreditjoin/{FormTransactionID}/datakreditjoin02/add");
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

				await F02Service.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
