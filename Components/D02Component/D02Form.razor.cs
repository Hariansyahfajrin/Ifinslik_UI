using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.D02Component
{
	public partial class D02Form
	{
		#region Service
		[Inject] D02Service D02Service { get; set; } = null!;
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; }
		[Inject] SysCompanyService SysCompanyService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string FormTransactionID { get; set; }
		#endregion
		SingleSelectLookup<MasterOJKReferenceModel>? JenisBadanUsahaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KodeDati2Lookup;
		SingleSelectLookup<MasterOJKReferenceModel>? NegaraDomisiliLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? BidangUsahaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? HubunganDenganPelaporLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? GolonganDebiturLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? MelangarMbpkLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? MelampauiBmpkLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? GoPublicLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? LembagaPemeringkatLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;

		#region Component Field
		#endregion

		#region Field
		public D02Model row = new();
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
			row = await D02Service.GetRowByID(ID) ?? new();

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
				var res = await D02Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/datadebiturbadanusaha/{FormTransactionID}/debitur02/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await D02Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/datadebiturbadanusaha/{FormTransactionID}");
		}
		#endregion

		protected async Task<List<MasterOJKReferenceModel>> LoadJenisBadanUsahaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISBADANUSAHA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKodeDati2Lookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEDATI2");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadNegaraDomisiliLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "NEGARADOMISILI");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadBidangUsahaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEBIDANGUSAHA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadHubunganDenganPelaporLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEHUBUNGANDENGANPELAPOR");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadGolonganDebiturLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEGOLONGANDEBITUR");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadMelangarMbpkLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "MELANGGARMBPK");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadMelampauiBmpkLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "MELAMPAUIBMPK");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadOperasiDataLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "OPERASIDATA");
		}
		protected async Task<List<SysCompanyModel>> LoadKantorCabangLookup(string keyword)
		{
			return await SysCompanyService.GetRowsForLookup(keyword, 0, 100);
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadGoPublicLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "GOPUBLIC");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadLembagaPemeringkatLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "LEMBAGAPEMERINGKAT");
		}
	}
}
