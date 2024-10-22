using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.TxtStatusOJKComponent
{
	public partial class TxtStatusOJKForm
	{
		#region Service
		[Inject] TxtStatusOJKService TxtStatusOJKService { get; set; } = null!;
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion

		#region Component Field
		SingleSelectLookup<BuktiBerhasilLaporModel>? PeriodeLookup;

		#endregion

		#region Field
		public TxtStatusOJKModel row = new();
		private readonly Dictionary<string, string> typeCompanys = new(){
			{"SYR","SYARIAH"},
			{"KVN","KONVENSIONAL"}
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
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await TxtStatusOJKService.GetRowByID(ID) ?? new();

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region LoadLookup
		protected async Task<List<BuktiBerhasilLaporModel>> LoadPeriodeLookup(string keyword)
		{
			return await BuktiBerhasilLaporService.GetRowsForLookup(keyword, 0, 100);
		}
		#endregion

		#region OnSubmit
		private async void OnSubmit()
		{
			Loading.Show();

			#region Insert
			if (ID == null)
			{
				var res = await TxtStatusOJKService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/generatetext/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await TxtStatusOJKService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo("/transaction/generatetext");
		}
		#endregion
	}
}
