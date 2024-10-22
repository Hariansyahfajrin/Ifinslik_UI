using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.K01HistoryComponent
{
	public partial class K01HistoryForm
	{
		#region Service
		[Inject] K01HistoryService K01HistoryService { get; set; } = null!;

		[Inject] D01HistoryService D01HistoryService { get; set; }
		[Inject] FormTransactionHistoryService FormTransactionHistoryService { get; set; }
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; }
		[Inject] SysCompanyService SysCompanyService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? FormTransactionHistoryID { get; set; }
		#endregion
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;
		SingleSelectLookup<D01HistoryModel>? CifLookup;

		#region Component Field
		#endregion

		#region Field
		public K01HistoryModel row = new();
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
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await K01HistoryService.GetRowByID(ID) ?? new();

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
				var res = await K01HistoryService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/inquiry/datalaporankeuangandebiturhistory/{FormTransactionHistoryID}/datalaporankeuangandebiturhistory01/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await K01HistoryService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/inquiry/datalaporankeuangandebiturhistory/{FormTransactionHistoryID}");
		}
		#endregion

		protected async Task<List<MasterOJKReferenceModel>> LoadOperasiDataLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "OPERASIDATA");
		}
		protected async Task<List<SysCompanyModel>> LoadKantorCabangLookup(string keyword)
		{
			return await SysCompanyService.GetRowsForLookup(keyword, 0, 100);
		}
		protected async Task<List<D01HistoryModel>> LoadCifLookup(string keyword)
		{
			return await D01HistoryService.GetRowsForLookup(keyword, 0, 100, FormTransactionHistoryID);
		}
	}
}
