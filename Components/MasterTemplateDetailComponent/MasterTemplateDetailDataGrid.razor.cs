using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.MasterTemplateDetailComponent
{
	public partial class MasterTemplateDetailDataGrid
	{
		#region Service
		[Inject] MasterTemplateDetailService MasterTemplateDetailService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? MasterTemplateID { get; set; }
		#endregion

		#region Component Field
		DataGrid<MasterTemplateDetailModel> dataGrid = null!;
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
		protected async Task<List<MasterTemplateDetailModel>?> LoadData(string keyword)
		{
			return await MasterTemplateDetailService.GetRows(keyword, 0, 100, MasterTemplateID);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/setting/mastertemplate/{MasterTemplateID}/mastertemplatedetail/add");
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

				await MasterTemplateDetailService.DeleteByID(dataGrid.selectedData.Select(row => row.ID ?? "").ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
