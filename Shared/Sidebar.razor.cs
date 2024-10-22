
using Config;
using Data.Service;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_UI.Shared
{
  public partial class Sidebar
  {
    [Inject] NavMenuService Service { get; set; } = null!;
    public string module = AppConfig.MODULE;
    private bool collapseNavMenu = true;
    private List<Data.Model.NavMenuModel>? menus;
    private string? SidebarCssClass => collapseNavMenu ? "collapse" : null;

    private new bool isLoading = false;

    private void ToggleNavMenu()
    {
      collapseNavMenu = !collapseNavMenu;
    }

    protected override async Task OnInitializedAsync()
    {
      isLoading = true;
      menus = await Service.GetAllMenu(module);

      isLoading = false;
      await base.OnInitializedAsync();
    }
  }
}