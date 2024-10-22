using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.SysGeneralSubCodeComponent
{
	public partial class SysGeneralSubCodeForm
	{
		#region Service
		[Inject] SysGeneralCodeService SysGeneralCodeService { get; set; } = null!;
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? GeneralCodeID { get; set; }
		[Parameter] public EventCallback<SysGeneralCodeModel> RowGeneralCodeChanged { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public SysGeneralSubCodeModel row = new();
		public SysGeneralCodeModel rowGeneralCode = new();
		public bool IsReadonly
		{
			get
			{
				return rowGeneralCode?.IsEditable == -1;
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
				row.IsActive = 1;
				row.SysGeneralCodeID = GeneralCodeID;
			}

			await GetRowGeneralCode();
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRowGeneralCode
		public async Task GetRowGeneralCode()
		{
			Loading.Show();

			rowGeneralCode = await SysGeneralCodeService.GetRowByID(GeneralCodeID) ?? new();
			await RowGeneralCodeChanged.InvokeAsync(rowGeneralCode);
			StateHasChanged();

			Loading.Close();
		}

		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();

			row = await SysGeneralSubCodeService.GetRowByID(ID) ?? new();
			StateHasChanged();

			Loading.Close();
		}
		#endregion

		#region ChangeStatus
		private async Task ChangeStatus()
		{
			if (ID != null)
			{
				Loading.Show();
				var res = await SysGeneralSubCodeService.ChangeStatus(row);

				if (res != null)
				{
					await GetRow();
				}

				Loading.Close();
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
				var res = await SysGeneralSubCodeService.Insert(row);

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/setting/generalcode/{GeneralCodeID}/generalsubcode/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				var res = await SysGeneralSubCodeService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/setting/generalcode/{GeneralCodeID}");
		}
		#endregion
	}
}
