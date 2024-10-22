using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.MasterTemplateComponent
{
	public partial class MasterTemplateDataGrid
	{
		#region Service
		[Inject] MasterTemplateService MasterTemplateService { get; set; } = null!;
		#endregion

		#region Component Field
		DataGrid<MasterTemplateModel> dataGrid = null!;
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
		protected async Task<List<MasterTemplateModel>?> LoadData(string keyword)
		{
			return await MasterTemplateService.GetRows(keyword, 0, 100);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo("/setting/mastertemplate/add");
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

				await MasterTemplateService.DeleteByID(dataGrid.selectedData.Select(row => row.ID ?? "").ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
