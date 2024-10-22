using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.A01Component
{
	public partial class A01Form
	{
		#region Service
		[Inject] A01Service A01Service { get; set; } = null!;
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
		SingleSelectLookup<MasterOJKReferenceModel>? JenisAgunanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisSegmenFasilitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? StatusAgunanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? LembagaPemeringkatLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KodeDati2Lookup;
		SingleSelectLookup<MasterOJKReferenceModel>? StatusParipasuLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? StatusKreditJoinLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? DiasuransikanLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisPengikatanLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;

		#region Component Field
		#endregion

		#region Field
		public A01Model row = new();
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
			row = await A01Service.GetRowByID(ID) ?? new();

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
				var res = await A01Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/dataagunan/{FormTransactionID}/agunan01/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await A01Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/dataagunan/{FormTransactionID}");
		}
		#endregion

		protected async Task<List<F01Model>> LoadNomorRekeningLookup(string keyword)
		{
			return await F01Service.GetRowsForLookup(keyword, 0, 100, FormTransactionID);
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisAgunanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISAGUNAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisSegmenFasilitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISSEGMENFASILITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadStatusAgunanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "GOLONGANPENJAMIN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadOperasiDataLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "OPERASIDATA");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadLembagaPemeringkatLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "LEMBAGAPEMERINGKAT");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKodeDati2Lookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEDATI2");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadStatusParipasuLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "STATUSPARIPASU");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadStatusKreditJoinLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "STATUSKREDITJOIN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadDiasuransikanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "DIASURANSIKAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisPengikatanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISPENGIKATAN");
		}
		protected async Task<List<SysCompanyModel>> LoadKantorCabangLookup(string keyword)
		{
			return await SysCompanyService.GetRowsForLookup(keyword, 0, 100);
		}
	}
}
