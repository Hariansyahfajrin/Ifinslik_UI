using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.SysCompanyComponent
{
	public partial class SysCompanyDataGrid
	{
		#region Service
		[Inject] SysCompanyService SysCompanyService { get; set; } = null!;
		#endregion

		#region Component Field
		DataGrid<SysCompanyModel> dataGrid = null!;
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
		protected async Task<List<SysCompanyModel>?> LoadData(string keyword)
		{
			return await SysCompanyService.GetRows(keyword, 0, 100);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/systemsetting/gllink/add");
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

				await SysCompanyService.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
