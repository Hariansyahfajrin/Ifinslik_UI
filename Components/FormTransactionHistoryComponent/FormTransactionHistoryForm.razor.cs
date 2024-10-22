using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.FormTransactionHistoryComponent
{
	public partial class FormTransactionHistoryForm
	{
		#region Service
		[Inject] FormTransactionHistoryService FormTransactionHistoryService { get; set; } = null!;

		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? Link { get; set; }
		#endregion
		SingleSelectLookup<SysGeneralSubCodeModel>? FormTypeLookup;

		#region Component Field
		#endregion

		#region Field
		public FormTransactionHistoryModel row = new();
		#endregion
		private Dictionary<string, string> typeCompanys = new(){
			{"SYR","SYARIAH"},
			{"KVN","KONVENSIONAL"}
		};

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
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await FormTransactionHistoryService.GetRowByID(ID) ?? new();

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region OnSubmit
		private async void OnSubmit()
		{
			Loading.Show();

			#region Insert
			if (ID == null)
			{
				var res = await FormTransactionHistoryService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/inquiry/{Link}/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await FormTransactionHistoryService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/inquiry/{Link}");
		}
		#endregion
		protected async Task<List<SysGeneralSubCodeModel>> LoadFormTypeLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "SLIKFT");
		}
	}
}
