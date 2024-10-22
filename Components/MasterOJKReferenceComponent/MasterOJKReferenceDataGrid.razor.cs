using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.MasterOJKReferenceComponent
{
	public partial class MasterOJKReferenceDataGrid
	{
		#region Service
		[Inject] MasterOJKReferenceService MasterOJKReferenceService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? GeneralCodeID { get; set; }
		[Parameter, EditorRequired] public string? GeneralSubCodeID { get; set; }
		[Parameter] public SysGeneralCodeModel? GeneralCode { get; set; }
		#endregion

		#region Component Field
		DataGrid<MasterOJKReferenceModel> dataGrid = null!;
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

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		protected async Task<List<MasterOJKReferenceModel>?> LoadData(string keyword)
		{
			return await MasterOJKReferenceService.GetRows(keyword, 0, 100, GeneralSubCodeID);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/setting/generalcode/{GeneralCodeID}/generalsubcode/{GeneralSubCodeID}/masterojkreference/add");
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

				await MasterOJKReferenceService.DeleteByID(dataGrid.selectedData.Select(row => row.ID ?? "").ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
