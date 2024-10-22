using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.P01Component
{
	public partial class P01Form
	{
		#region Service
		[Inject] P01Service P01Service { get; set; } = null!;
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
		#region Component Field
		#endregion

		#region Field
		public P01Model row = new();
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
			row = await P01Service.GetRowByID(ID) ?? new();

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
				var res = await P01Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/datapenjamin/{FormTransactionID}/Penjamin02/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await P01Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back

		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/datapenjamin/{FormTransactionID}");
		}

		#endregion
		protected async Task<List<F01Model>> LoadNomorRekeningLookup(string keyword)
		{
			return await F01Service.GetRowsForLookup(keyword, 0, 100, FormTransactionID);
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
	}
}
