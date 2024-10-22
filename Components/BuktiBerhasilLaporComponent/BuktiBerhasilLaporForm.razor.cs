using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.BuktiBerhasilLaporComponent
{
	public partial class BuktiBerhasilLaporForm
	{
		#region Service
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public BuktiBerhasilLaporModel row = new();
		private Dictionary<string, string> companyTypes = new(){
			{"SYR","SYARIAH"},
			{"KVN","KONVENSIONAL"}
		};
		private Dictionary<string, string> months = new(){
				{"01",    "Januari" },
				{"02",    "Februari" },
				{"03",    "Maret" },
				{"04",    "April" },
				{"05",    "Mei" },
				{"06",    "Juni" },
				{"07",    "Juli" },
				{"08",    "Agustus" },
				{"09",    "September" },
				{"10",    "Oktober" },
				{"11",    "November" },
				{"12",    "Desember" },
		};

		private Dictionary<string, string> years = new(){
					{ "2020","2020" },
					{ "2021", "2021" },
					{ "2022", "2022" },
					{ "2023", "2023" },
					{ "2024", "2024" },
					{ "2025", "2025" },
					{ "2026", "2026" },
					{ "2027", "2027" },
					{ "2028", "2028" },
					{ "2029", "2029" },
					{ "2030", "2030" },
		};
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
				row.Status = "HOLD";
				row.IsActive = 1;
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await BuktiBerhasilLaporService.GetRowByID(ID) ?? new();

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
				var res = await BuktiBerhasilLaporService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/periodepelaporanojk/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await BuktiBerhasilLaporService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region ChangeStatus
		private async Task ChangeStatus()
		{
			if (ID != null)
			{
				Loading.Show();
				var res = await BuktiBerhasilLaporService.ChangeStatus(row);

				if (res != null)
				{
					await GetRow();
				}

				Loading.Close();
				StateHasChanged();
			}
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo("/transaction/periodepelaporanojk");
		}
		#endregion
	}
}
