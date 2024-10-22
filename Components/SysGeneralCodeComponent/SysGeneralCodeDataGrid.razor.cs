using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.SysGeneralCodeComponent
{
	public partial class SysGeneralCodeDataGrid
	{
		#region Service
		[Inject] SysGeneralCodeService SysGeneralCodeService { get; set; } = null!;
		#endregion

		#region Component Field
		DataGrid<SysGeneralCodeModel> dataGrid = null!;
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
		protected async Task<List<SysGeneralCodeModel>?> LoadData(string keyword)
		{
			return await SysGeneralCodeService.GetRows(keyword, 0, 100);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/setting/generalcode/add");
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

				await SysGeneralCodeService.DeleteByID(dataGrid.selectedData.Select(row => row.ID ?? "").ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
