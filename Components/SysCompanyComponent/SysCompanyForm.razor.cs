using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SLIK_UI.Components.SysCompanyComponent
{
	public partial class SysCompanyForm
	{
		#region Service
		[Inject] SysCompanyService SysCompanyService { get; set; } = null!;
		#endregion

		#region Parameter
		[Parameter, EditorRequired] public string? ID { get; set; }
		#endregion

		#region Component Field
		#endregion

		#region Field
		public SysCompanyModel row = new();
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
		row = await SysCompanyService.GetRowByID(ID) ?? new();

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
			var res = await SysCompanyService.Insert(row);

			Loading.Close();

			if (res?.Data != null)
			{
				NavigationManager.NavigateTo($"/systemsetting/gllink/{res.Data.ID}", true);
			}
		}
		#endregion

		#region Update
		else
		{
			await SysCompanyService.UpdateByID(row);
		}
		#endregion

		Loading.Close();
		StateHasChanged();
	}
	#endregion

	#region Back
	private void Back()
	{
		NavigationManager.NavigateTo("/systemsetting/gllink");
	}
	#endregion
	}
}
