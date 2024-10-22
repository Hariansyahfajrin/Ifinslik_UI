using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.MasterOJKReferenceComponent
{
	public partial class MasterOJKReferenceForm
	{
		#region Service
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; } = null!;
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		[Inject] SysGeneralCodeService SysGeneralCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? GeneralCodeID { get; set; }
		[Parameter, EditorRequired] public string? GeneralSubCodeID { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public MasterOJKReferenceModel row = new();
		public SysGeneralSubCodeModel rowSubCode = new();
		public SysGeneralCodeModel rowGeneralCode = new();
		public bool IsReadonly
		{
			get
			{
				return rowGeneralCode.IsEditable == -1;
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
				// Default value
				row.ReferenceTypeID = GeneralSubCodeID;
				row.IsActive = 1;
			}
			rowSubCode = await SysGeneralSubCodeService.GetRowByID(GeneralSubCodeID) ?? new();
			rowGeneralCode = await SysGeneralCodeService.GetRowByID(GeneralCodeID) ?? new();
			await base.OnInitializedAsync();
		}
		#endregion

		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await MasterOJKReferenceService.GetRowByID(ID) ?? new();

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
				var res = await MasterOJKReferenceService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/setting/generalcode/{GeneralCodeID}/generalsubcode/{GeneralSubCodeID}/masterojkreference/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await MasterOJKReferenceService.UpdateByID(row);
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
				var res = await MasterOJKReferenceService.ChangeStatus(row);

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
			NavigationManager.NavigateTo($"/setting/generalcode/{GeneralCodeID}/generalsubcode/{GeneralSubCodeID}");
		}
		#endregion
	}
}
