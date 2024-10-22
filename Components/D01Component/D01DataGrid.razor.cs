using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.D01Component
{
	public partial class D01DataGrid
	{
		#region Service
		[Inject] D01Service D01Service { get; set; } = null!;
		#endregion

		#region Parameters
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		#region Component Fields
		DataGrid<D01Model> dataGrid = null!;
		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		protected async Task<List<D01Model>?> LoadData(string keyword)
		{
			return await D01Service.GetRows(keyword, 0, 100, FormTransactionID ?? "");
		}
		#endregion

		private void Back()
		{
			NavigationManager.NavigateTo("/transaction/datadebiturperorangan");
		}

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/datadebiturperorangan/{FormTransactionID}/debitur01/add");
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

				await D01Service.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
