using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.F06Component
{
	public partial class F06Form
	{
		#region Service
		[Inject] F06Service F06Service { get; set; } = null!;
		[Inject] F01Service F01Service { get; set; }
		[Inject] FormTransactionService FormTransactionService { get; set; }
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; }
		[Inject] SysCompanyService SysCompanyService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		SingleSelectLookup<F01Model>? NomorRekeningLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisIdentitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisSegmenFasilitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? GolonganPenjaminLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? ValutaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SumberDanaLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KondisiLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? SebabMacetLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KolektibilitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisFasilitasLainnyaLookup;

		#region Component Field
		#endregion

		#region Field
		public F06Model row = new();
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
			row = await F06Service.GetRowByID(ID) ?? new();

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
				var res = await F06Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/datafasilitas/{FormTransactionID}/datafasilitas06/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await F06Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/datafasilitas/{FormTransactionID}");
		}
		#endregion

		protected async Task<List<MasterOJKReferenceModel>> LoadSebabMacetLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SEBABMACET");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKondisiLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KONDISI");
		}
		protected async Task<List<F01Model>> LoadNomorRekeningLookup(string keyword)
		{
			return await F01Service.GetRowsForLookup(keyword, 0, 100, FormTransactionID);
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKolektibilitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KOLEKTIBILITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisIdentitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISIDENTITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisSegmenFasilitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISSEGMENFASILITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadGolonganPenjaminLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "GOLONGANPENJAMIN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadOperasiDataLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "OPERASIDATA");
		}
		protected async Task<List<SysCompanyModel>> LoadKantorCabangLookup(string keyword)
		{
			return await SysCompanyService.GetRowsForLookup(keyword, 0, 100);
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadValutaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "VALUTA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadSumberDanaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "SUMBERDANA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisFasilitasLainnyaLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISFASILITASLAINNYA");
		}
	}
}
