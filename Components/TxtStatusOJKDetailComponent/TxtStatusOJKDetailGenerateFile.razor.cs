using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.TxtStatusOJKDetailComponent
{
	public partial class TxtStatusOJKDetailGenerateFile
	{
		#region Service
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? TxtStatusOJKID { get; set; }
		#endregion

		#region Component Field
		SingleSelectLookup<SysGeneralSubCodeModel>? FormTypeLookup;
		SingleSelectLookup<SysGeneralSubCodeModel>? SlikGroupLookup;
		#endregion

		#region Field
		public TxtStatusOJKDetailModel row = new();
		#endregion

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}


		#region Load Lookup
		protected async Task<List<SysGeneralSubCodeModel>> LoadFormTypeLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "SLIKFT");
		}
		protected async Task<List<SysGeneralSubCodeModel>> LoadSlikGroupLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "SLIKGR");
		}
		#endregion
	}
}