using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.M01Component
{
	public partial class M01Form
	{
		#region Service
		[Inject] M01Service M01Service { get; set; } = null!;
		[Inject] D02Service D02Service { get; set; }
		[Inject] FormTransactionService FormTransactionService { get; set; }
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; }
		[Inject] SysCompanyService SysCompanyService { get; set; }

		#endregion
		SingleSelectLookup<MasterOJKReferenceModel>? JenisIdentitasLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? StatusPengurusLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JenisKelaminLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? KodeDati2Lookup;
		SingleSelectLookup<MasterOJKReferenceModel>? OperasiDataLookup;
		SingleSelectLookup<MasterOJKReferenceModel>? JabatanLookup;
		SingleSelectLookup<SysCompanyModel>? KantorCabangLookup;
		SingleSelectLookup<D02Model>? CifLookup;

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public M01Model row = new();
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
			row = await M01Service.GetRowByID(ID) ?? new();

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
				var res = await M01Service.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/datapengurus/{FormTransactionID}/pengurus01/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await M01Service.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/datapengurus/{FormTransactionID}");
		}
		#endregion
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisIdentitasLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISIDENTITAS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadStatusPengurusLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "STATUSPENGURUS");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadKodeDati2Lookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "KODEDATI2");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJabatanLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JABATAN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadJenisKelaminLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "JENISKELAMIN");
		}
		protected async Task<List<MasterOJKReferenceModel>> LoadOperasiDataLookup(string keyword)
		{
			return await MasterOJKReferenceService.GetRowsForLookup(keyword, 0, 100, "OPERASIDATA");
		}
		protected async Task<List<SysCompanyModel>> LoadKantorCabangLookup(string keyword)
		{
			return await SysCompanyService.GetRowsForLookup(keyword, 0, 100);
		}
		protected async Task<List<D02Model>> LoadCifLookup(string keyword)
		{
			return await D02Service.GetRowsForLookup(keyword, 0, 100, FormTransactionID);
		}
	}
}
