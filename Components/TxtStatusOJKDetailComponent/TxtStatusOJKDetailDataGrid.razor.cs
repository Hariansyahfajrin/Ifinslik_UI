using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.TxtStatusOJKDetailComponent
{
	public partial class TxtStatusOJKDetailDataGrid
	{
		#region Service
		[Inject] TxtStatusOJKDetailService TxtStatusOJKDetailService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? TxtStatusOJKID { get; set; }
		#endregion

		#region Component Field
		DataGrid<TxtStatusOJKDetailModel> dataGrid = null!;
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
		protected async Task<List<TxtStatusOJKDetailModel>?> LoadData(string keyword)
		{
			return await TxtStatusOJKDetailService.GetRows(keyword, 0, 100, TxtStatusOJKID ?? "");
		}
		#endregion

		#region Validate
		async void Validate()
		{
			await Alert("Maintanance", "Under Maintanance");
		}
		#endregion

	}
}
