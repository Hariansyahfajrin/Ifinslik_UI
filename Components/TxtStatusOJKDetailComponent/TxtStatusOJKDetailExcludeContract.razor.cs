using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.TxtStatusOJKDetailComponent
{
	public partial class TxtStatusOJKDetailExcludeContract
	{
		#region Service
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		[Inject] TxtStatusOJKDetailService TxtStatusOJKDetailService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FinanceCompanyType { get; set; }
		#endregion

		#region Component Field
		DataGrid<TxtStatusOJKDetailModel>? dataGrid;
		SingleSelectLookup<SysGeneralSubCodeModel>? DescriptionLookup;
		SingleSelectLookup<SysGeneralSubCodeModel>? SlikGroupLookup;
		#endregion

		#region Field
		public TxtStatusOJKDetailModel row = new();
		#endregion

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}

		#region LoadData
		protected async Task<List<TxtStatusOJKDetailModel>?> LoadData(string keyword)
		{
			return await TxtStatusOJKDetailService.GetRows(keyword, 0, 100, FinanceCompanyType);
		}
		#endregion


		#region Load Lookup
		protected async Task<List<SysGeneralSubCodeModel>> LoadDescriptionLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "SLIKEXT");
		}
		protected async Task<List<SysGeneralSubCodeModel>> LoadSlikGroupLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "SLIKGR");
		}
		#endregion

		#region Download
		async void Download()
		{
			await Alert("Maintanance", "Under Maintanance");
		}
		#endregion

		#region Upload
		async void Upload()
		{
			await Alert("Maintanance", "Under Maintanance");
		}
		#endregion
	}
}