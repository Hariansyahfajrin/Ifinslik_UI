using Data.Model;
using Data.Service;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.SysGeneralCodeComponent
{
	public partial class SysGeneralCodeForm
	{
		#region Service
		[Inject] SysGeneralCodeService SysGeneralCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter] public string? ID { get; set; }
		[Parameter] public EventCallback<SysGeneralCodeModel> RowChanged { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public SysGeneralCodeModel row = new();
		public bool IsReadonly
		{
			get
			{
				return row.IsEditable == -1;
			}
		}
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
				row.IsEditable = 1;
			}
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await SysGeneralCodeService.GetRowByID(ID) ?? new();
			await RowChanged.InvokeAsync(row);
			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region ChangeEditable
		private async Task ChangeEditable()
		{
			if (ID != null)
			{
				Loading.Show();
				var res = await SysGeneralCodeService.ChangeStatus(row);

				if (res != null)
				{
					await GetRow();
					Loading.Close();
				}

				StateHasChanged();
			}
		}
		#endregion

		#region OnSubmit
		private async void OnSubmit()
		{
			Loading.Show();

			#region Insert
			if (ID == null)
			{
				var res = await SysGeneralCodeService.Insert(row);

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/setting/generalcode/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await SysGeneralCodeService.UpdateByID(row);
			}

			Loading.Close();
			StateHasChanged();
			#endregion
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo("/setting/generalcode");
		}
		#endregion

	}
}
