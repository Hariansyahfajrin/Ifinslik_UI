using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.D01Component
{
	public partial class D01Form
	{
		#region Service
		[Inject] D01Service D01Service { get; set; } = null!;
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; } = null!;
		[Inject] SysCompanyService SysCompanyService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion
		[Parameter, EditorRequired] public string FormTransactionID { get; set; }
		SingleSelectLookup<MasterOJKReferenceModel>? JenisIdentitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? StatusPendidikanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisKelaminLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KodeDati2Lookup;
		SingleSelectLookup<MasterOJKReferenceModel>? NegaraDomisiliLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KodePekerjaanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? BidangUsahaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SumberPenghasilanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? HubunganDenganPelaporLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? GolonganDebiturLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? StatusPerkawinanDebiturLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? PerjanjianPisahHartaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? MelangarMbpkLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? MelampauiBmpkLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;

		#region Component Field
		#endregion

		#region Field
		public D01Model row = new();
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
				row.FormTransactionID = FormTransactionID;
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await D01Service.GetRowByID(ID) ?? new();

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
				var res = await D01Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/datadebiturperorangan/{FormTransactionID}/debitur01/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await D01Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/datadebiturperorangan/{FormTransactionID}");
		}
		#endregion
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisIdentitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISIDENTITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadStatusPendidikanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "STATUSPENDIDIKAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKodeDati2Lookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEDATI2");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadNegaraDomisiliLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "NEGARADOMISILI");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisKelaminLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISKELAMIN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKodePekerjaanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEPEKERJAAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadBidangUsahaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEBIDANGUSAHA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSumberPenghasilanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODESUMBERPENGHASILAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadHubunganDenganPelaporLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEHUBUNGANDENGANPELAPOR");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadGolonganDebiturLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEGOLONGANDEBITUR");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadStatusPerkawinanDebiturLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "STATUSPERKAWINANDEBITUR");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadPerjanjianPisahHartaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "PERJANJIANPISAHHARTA");
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
	}
}
