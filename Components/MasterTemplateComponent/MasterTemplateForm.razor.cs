using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.MasterTemplateComponent
{
	public partial class MasterTemplateForm
	{
		#region Service
		[Inject] MasterTemplateService MasterTemplateService { get; set; } = null!;
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion

		#region Component Field
		SingleSelectLookup<SysGeneralSubCodeModel>? FormTypeLookup;
		#endregion

		#region Field
		public MasterTemplateModel row = new();
		private readonly Dictionary<string, string> typeCompanys = new(){
			{"SYR","SYARIAH"},
			{"KVN","KONVENSIONAL"}
		};
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
				// Default value
				row.IsActive = 1;
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await MasterTemplateService.GetRowByID(ID) ?? new();

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region LoadFormTypeLookup
		protected async Task<List<SysGeneralSubCodeModel>> LoadFormTypeLookup(string keyword)
		{
			string code;
			if (row.FinanceCompanyType == "KVN")
			{
				code = "SLIKKT";
			}
			else
			{
				code = "SLIKST";
			}
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, code);
		}
		#endregion

		#region OnSubmit
		private async void OnSubmit()
		{
			Loading.Show();

			#region Insert
			if (ID == null)
			{
				var res = await MasterTemplateService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/setting/mastertemplate/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await MasterTemplateService.UpdateByID(row);
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
				var res = await MasterTemplateService.ChangeStatus(row);

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
			NavigationManager.NavigateTo("/setting/mastertemplate");
		}
		#endregion
	}
}
