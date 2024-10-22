using Data.Model;
using Data.Service;
using Microsoft.AspNetCore.Components;
using IFinancing360_UI.Components;

namespace IFinancing360_SLIK_UI.Components.SysGlobalParamComponent
{
	public partial class SysGlobalParamDataGrid
	{
		#region Service
		[Inject] SysGlobalParamService SysGlobalParamService { get; set; } = null!;
		#endregion

		#region Component Field
		DataGrid<SysGlobalParamModel> dataGrid = null!;
		#endregion
		protected async Task<List<SysGlobalParamModel>?> LoadData(string keyword)
		{
			return await SysGlobalParamService.GetRows(keyword, 0, 100);
		}

		private void Add()
		{
			NavigationManager.NavigateTo($"/controlpanel/globalparam/add");
		}

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

				await SysGlobalParamService.DeleteByID(selectedData.Select(row => row.ID ?? "").ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
	}
}