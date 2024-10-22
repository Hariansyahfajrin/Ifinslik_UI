using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.SysGeneralSubCodeComponent
{
	public partial class SysGeneralSubCodeDataGrid
	{
		#region Service
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? GeneralCodeID { get; set; }
		[Parameter] public SysGeneralCodeModel? GeneralCode { get; set; } = new();
		#endregion

		#region Component Field
		DataGrid<SysGeneralSubCodeModel> dataGrid = null!;
		#endregion

		#region Field
		public bool IsReadonly
		{
			get
			{
				return GeneralCode?.IsEditable == -1;
			}
		}
		#endregion

		#region LoadData
		protected async Task<List<SysGeneralSubCodeModel>?> LoadData(string keyword)
		{
			return await SysGeneralSubCodeService.GetRows(keyword, 0, 100, GeneralCodeID ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/setting/generalcode/{GeneralCodeID}/generalsubcode/add");
		}
		#endregion

		#region Delete
		private async void Delete()
		{
			var selectedData = dataGrid.selectedData;

			if (!selectedData.Any())
			{
				await NoDataSelectedAlert();
				return;
			}

			bool? result = await Confirm();

			if (result == true)
			{
				Loading.Show();

				await SysGeneralSubCodeService.DeleteByID(dataGrid.selectedData.Select(row => row.ID ?? "").ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
