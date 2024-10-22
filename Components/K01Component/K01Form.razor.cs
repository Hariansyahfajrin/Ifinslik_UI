using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.K01Component
{
	public partial class K01Form
	{
		#region Service
		[Inject] K01Service K01Service { get; set; } = null!;
		[Inject] D01Service D01Service { get; set; }
		[Inject] FormTransactionService FormTransactionService { get; set; }
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; }
		[Inject] SysCompanyService SysCompanyService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;
		SingleSelectLookup<D01Model>? CifLookup;

		#region Component Field
		#endregion

		#region Field
		public K01Model row = new();
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
				row.FormTransactionID = FormTransactionID;
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await K01Service.GetRowByID(ID) ?? new();

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
				var res = await K01Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/datalaporankeuangandebitur/{FormTransactionID}/datalaporankeuangandebitur01/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await K01Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/datalaporankeuangandebitur/{FormTransactionID}");
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
		protected async Task<List<D01Model>> LoadCifLookup(string keyword)
		{
			return await D01Service.GetRowsForLookup(keyword, 0, 100, FormTransactionID);
		}

	}
}
