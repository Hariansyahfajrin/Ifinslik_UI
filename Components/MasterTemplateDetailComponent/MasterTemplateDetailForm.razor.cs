using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.MasterTemplateDetailComponent
{
	public partial class MasterTemplateDetailForm
	{
		#region Service
		[Inject] MasterTemplateDetailService MasterTemplateDetailService { get; set; } = null!;
		[Inject] MasterTemplateService MasterTemplateService { get; set; } = null!;
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? MasterTemplateID { get; set; }
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion

		#region Component Field
		SingleSelectLookup<SysGeneralSubCodeModel> formTypeLookup = null!;
		SingleSelectLookup<SysGeneralSubCodeModel> refferenceTypeLookup = null!;
		#endregion

		#region Field
		public MasterTemplateDetailModel row = new();
		public MasterTemplateModel rowTemplate = new();
		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			if (ID != null)
			{
				await GetRow();
			}
			else
			{
				row = new MasterTemplateDetailModel { MasterTemplateID = MasterTemplateID };
				row.TemplateCode = rowTemplate.Code;
				row.IsActive = 1;
			}
			rowTemplate = await MasterTemplateService.GetRowByID(MasterTemplateID) ?? new();
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await MasterTemplateDetailService.GetRowByID(ID) ?? new();

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region LoadLookup
		protected async Task<List<SysGeneralSubCodeModel>> LoadFormTypeLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "TFT");
		}
		protected async Task<List<SysGeneralSubCodeModel>> LoadRefferenceTypeLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "OJKRFR");
		}
		#endregion

		#region OnSubmit
		private async void OnSubmit()
		{
			Loading.Show();

			#region Insert
			if (ID == null)
			{
				var res = await MasterTemplateDetailService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/setting/mastertemplate/{MasterTemplateID}/mastertemplatedetail/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await MasterTemplateDetailService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region ChangeStatus
		private async Task ChangeStatus()
		{
			if (ID != null)
			{
				Loading.Show();
				var res = await MasterTemplateDetailService.ChangeStatus(row);

				if (res != null)
				{
					await GetRow();
				}

				Loading.Close();
				StateHasChanged();
			}
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/setting/mastertemplate/{MasterTemplateID}");
		}
		#endregion
	}
}
