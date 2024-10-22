using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.TxtStatusOJKDetailComponent
{
	public partial class TxtStatusOJKDetailForm
	{
		#region Service
		[Inject] TxtStatusOJKDetailService TxtStatusOJKDetailService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public TxtStatusOJKDetailModel row = new();
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
			row = await TxtStatusOJKDetailService.GetRowByID(ID) ?? new();

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
				var res = await TxtStatusOJKDetailService.Insert(row);

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
				await TxtStatusOJKDetailService.UpdateByID(row);
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
