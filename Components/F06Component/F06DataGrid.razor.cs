using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.F06Component
{
	public partial class F06DataGrid
	{
		#region Service
		[Inject] F06Service F06Service { get; set; } = null!;
		[Inject] FormTransactionService FormTransactionService { get; set; }
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; }
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; }
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormTransactionID { get; set; }
		#endregion

		#region Component Field
		DataGrid<F06Model> dataGrid = null!;
		#endregion

		#region Field

		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		protected async Task<List<F06Model>?> LoadData(string keyword)
		{
			return await F06Service.GetRows(keyword, 0, 100, FormTransactionID ?? "");
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/datafasilitas/{FormTransactionID}/datafasilitas06/add");
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

				await F06Service.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
