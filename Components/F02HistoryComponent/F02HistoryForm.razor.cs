using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.F02HistoryComponent
{
	public partial class F02HistoryForm
	{
		#region Service
		[Inject] F02HistoryService F02HistoryService { get; set; } = null!;
		[Inject] FormTransactionHistoryService FormTransactionHistoryService { get; set; }
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; }
		[Inject] SysCompanyService SysCompanyService { get; set; }
		[Inject] D01HistoryService D01HistoryService { get; set; }
		#endregion
		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? FormTransactionHistoryID { get; set; }
		#endregion
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KolektibilitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SifatKreditLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisKreditLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SkimLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? BaruPerpanjanganLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KategoriDebiturLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisPenggunaanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? OrientasiPenggunaanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SektorEkonomiLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KodeDati2Lookup;
		SingleSelectLookup<MasterOJKReferenceModel>? ValutaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisSukuBungaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KreditProgramPemerintahLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? TakeoverDariLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SumberDanaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SebabMacetLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? CaraRestrukturisasiLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KondisiLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;
		SingleSelectLookup<D01HistoryModel>? CifLookup;



		#region Component Field
		#endregion

		#region Field
		public F02HistoryModel row = new();
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
			row = await F02HistoryService.GetRowByID(ID) ?? new();

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
				var res = await F02HistoryService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/inquiry/datakreditjoinhistory/{FormTransactionHistoryID}/datakreditjoinhistory02/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await F02HistoryService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/inquiry/datakreditjoinhistory/{FormTransactionHistoryID}");
		}
		#endregion

		protected async Task<List<D01HistoryModel>> LoadCifLookup(string keyword)
		{
			return await D01HistoryService.GetRowsForLookup(keyword, 0, 100, FormTransactionHistoryID);
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadOperasiDataLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "OPERASIDATA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSifatKreditLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SIFATKREDIT");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisKreditLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISKREDIT");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSkimLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SKIM");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKolektibilitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KOLEKTIBILITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadBaruPerpanjanganLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "BARUPERPANJANGAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKategoriDebiturLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KATEGORIDEBITUR");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisPenggunaanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISPENGGUNAAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadOrientasiPenggunaanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "ORIENTASIPENGGUNAAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSektorEkonomiLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SEKTOREKONOMI");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadValutaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "VALUTA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisSukuBungaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISSUKUBUNGA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKreditProgramPemerintahLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KREDITPROGRAMPEMERINTAH");
		}

		protected async Task<List<MasterOJKReferenceModel>> LoadTakeoverDariLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "TAKEOVERDARI");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSumberDanaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SUMBERDANA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSebabMacetLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SEBABMACET");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadCaraRestrukturisasiLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "CARARESTRUKTURISASI");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKondisiLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KONDISI");
		}
		protected async Task<List<SysCompanyModel>> LoadKantorCabangLookup(string keyword)
		{
			return await SysCompanyService.GetRowsForLookup(keyword, 0, 100);
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKodeDati2Lookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEDATI2");
		}
	}
}
