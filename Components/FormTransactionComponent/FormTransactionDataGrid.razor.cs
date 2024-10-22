using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.FormTransactionComponent
{
	public partial class FormTransactionDataGrid
	{
		#region Service
		[Inject] FormTransactionService FormTransactionService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? FormType { get; set; }
		[Parameter, EditorRequired] public string? Link { get; set; }
		#endregion

		#region Component Field
		DataGrid<FormTransactionModel> dataGrid = null!;
		#endregion

		#region Field
		public FormTransactionModel filter = new()
		{
			Date = new DateTime(2023, 12, 31)
		};
		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		protected async Task<List<FormTransactionModel>?> LoadData(string keyword)
		{
			return await FormTransactionService.GetRows(keyword, 0, 100, FormType ?? "", filter.Date);
		}
		#endregion

		#region Add
		private void Add()
		{
			NavigationManager.NavigateTo($"/transaction/{Link}/add");
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

				await FormTransactionService.DeleteByID(dataGrid.selectedData.Select(row => row.ID).ToArray());

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				Loading.Close();

				StateHasChanged();
			}
		}
		#endregion
	}
}
