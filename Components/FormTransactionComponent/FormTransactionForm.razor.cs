using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.FormTransactionComponent
{
	public partial class FormTransactionForm
	{
		#region Service
		[Inject] FormTransactionService FormTransactionService { get; set; } = null!;
		[Inject] BuktiBerhasilLaporService BuktiBerhasilLaporService { get; set; } = null!;
		[Inject] SysGeneralSubCodeService SysGeneralSubCodeService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		[Parameter, EditorRequired] public string? Link { get; set; }
		[Parameter] public string? FormType { get; set; }
		#endregion

		#region Component Field
		#endregion

		SingleSelectLookup<SysGeneralSubCodeModel>? FormTypeLookup;
		SingleSelectLookup<BuktiBerhasilLaporModel>? PeriodePelaporanLookup;

		#region Field
		public FormTransactionModel row = new();
		public DateTime? MinDate
		{
			get
			{
				var date = row.Date;
				if (date == null)
				{
					return null;
				}

				return new DateTime(date.Value.Year, date.Value.Month, 1);
			}
		}
		public DateTime? MaxDate
		{
			get
			{
				var date = new DateTime(row.Date?.Year ?? DateTime.Now.Year, row.Date?.Month ?? DateTime.Now.Month, 1);
				date = date.AddMonths(1);
				return date.AddDays(-1);
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
				row.FormType = FormType;
				row.SysGeneralSubCodeFormType = (await SysGeneralSubCodeService.GetRowByCode(FormType))?.Description;
			}
			await base.OnInitializedAsync();
		}
		#endregion
		DataGrid<Data.Model.FormTransactionModel>? dataGrid;
		private Dictionary<string, string> typeCompanys = new(){
			{"KVN","KONVENSIONAL"},
			{"SYR","SYARIAH"},
		};
		#region GetRow
		public async Task GetRow()
		{
			Loading.Show();
			row = await FormTransactionService.GetRowByID(ID) ?? new();

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
				var res = await FormTransactionService.Insert(row);

				Loading.Close();

				if (res?.Data != null)
				{
					NavigationManager.NavigateTo($"/transaction/{Link}/{res.Data.ID}", true);
				}
			}
			#endregion

			#region Update
			else
			{
				await FormTransactionService.UpdateByID(row);
			}
			#endregion

			Loading.Close();
			StateHasChanged();
		}
		#endregion

		#region Back
		private void Back()
		{
			NavigationManager.NavigateTo($"/transaction/{Link}");
		}

		#endregion
		protected async Task<List<SysGeneralSubCodeModel>> LoadFormTypeLookup(string keyword)
		{
			return await SysGeneralSubCodeService.GetRowsForLookup(keyword, 0, 100, "SLIKFT");
		}
		protected async Task<List<BuktiBerhasilLaporModel>> LoadPeriodePelaporanLookup(string keyword)
		{
			return await BuktiBerhasilLaporService.GetRowsForLookup(keyword, 0, 100);
		}
	}
}
