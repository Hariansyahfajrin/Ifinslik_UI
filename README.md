# IFinancing360 UI

## Model

Model merupakan representasi Object Data yang digunakan saat penerimaan ataupun pengiriman data ke API. Sebaiknya **Model disamakan dengan Model yang ada di API**.
Pastikan Model. Peletakan File Model diletakkan pada _Data\Model_ dengan `Namespace Data.Model`. Model Inherit terhadap `BaseModel`. Contoh berikut merupakan contoh model `SysMenu` pada Module `IFINSYS`

```cs
// File: Data/Model/SysMenu.cs

namespace Data.Model
{
	public class SysMenu : BaseModel
	{
		public string? Code { get; set; }
		public string? Name { get; set; }
		public string? ModuleID { get; set; }
		public string? ParentMenuID { get; set; }
		public string? Abbreviation { get; set; };
		public string? RoutingMenu { get; set; }
		public string? URLMenu { get; set; }
		public int? OrderKey { get; set; }
		public string? CssIcon { get; set; };
		public int? IsActive { get; set; }
		public string? Type { get; set; }
		public SysModule? Module { get; set; }
		public SysMenu? Parent { get; set; }

	}
}
```

## Service

**Service** merupakan _class_ yang berisikan **method** untuk melakukan **pemanggilan API** oleh karena itu, umumnya **Service** memiliki **5 _method_** yaitu:

- `GetRows` : Return List Data
- `GetRow` : Return Single Data
- `Insert` : Penambahan Data
- `Update` : Ubah Data
- `Delete` : Hapus Data

Tentu _method_ yang ada pada **Service** bergantung pada **ketersediaan API**. `Service` haruslah memiliki **Attribute** `[Service]`. Untuk dapat menggunakan API, **Service** harus melakukan **\*Inject HTTP Client** yang akan digunakan dengan menambahkan **class** HTTP Client sebagai parameter `Constructor`. Contoh untuk `SysMenuService` berikut akan menggunakan `IFINSYSClient` yang merupakan HTTP Client untuk API `IFINSYS`.

```cs
// File: Data/Service/SysMenuService.cs

using Helper.HTTPService;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysMenuService
	{
		// IFINSYS HTTP Client Field
		public readonly IFINSYSClient _ifinsysClient;

		// Constructor dengan Inject IFINSYSClient
		public SysMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		/*
		* METHODS DIBAWAH CONSTRUCTOR
		*/
	}
}
```

### Contoh Method Service

`NOTE` :

- Parameters bersifat **_Case Sensitive_**, pastikan penulisan parameters sesuai dengan penulisan parameters pada API termasuk besar kecil huruf.
- Penggunaan **HTTP Client** mengikuti dengan **HTTP Method** yang digunakan pada API.

#### GetRows

```cs
// File: Data/Service/SysMenuService.cs

using Helper.HTTPService;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysMenuService
	{
		// IFINSYS HTTP Client
		public readonly IFINSYSClient _ifinsysClient;

		// API Controller
		public readonly string _apiController = "SysMenu";

		// API Route
		public readonly string _apiRouteGetRows = "GetRows";

		// Constructor
		public SysMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		public async Task<List<SysMenu>?> GetRows(string keyword, int offset, int limit)
		{
			// Deklarasi parameters
			var parameters = new
			{
				Keyword = keyword,
				Offset = offset,
				Limit = limit
			};

			// Get List Data
			var res = await _ifinsysClient.GetRows<SysMenu>(_apiController, _apiRouteGetRows, parameters);

			return res?.Data;
		}
	}
}
```

### GetRowByID

```cs
// File: Data/Service/SysMenuService.cs

using Helper.HTTPService;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysMenuService
	{
		// IFINSYS HTTP Client
		public readonly IFINSYSClient _ifinsysClient;

		// API Controller
		public readonly string _apiController = "SysMenu";

		// API Route
		public readonly string _apiRouteGetRow = "GetRow";

		// Constructor
		public SysMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		public async Task<SysMenu?> GetRowByID(string? id)
		{
			// Deklarasi parameters
			var parameters = new
			{
				ID = id
			};

			// Get Single Data
			var res = await _ifinsysClient.GetRow<SysMenu>(_apiController, _apiRouteGetRow, parameters);

			return res?.Data;
		}
	}
}
```

### Insert

```cs
// File: Data/Service/SysMenuService.cs

using Helper.HTTPService;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysMenuService
	{
		// IFINSYS HTTP Client
		public readonly IFINSYSClient _ifinsysClient;

		// API Controller
		public readonly string _apiController = "SysMenu";

		// API Route
		public readonly string _apiRouteInsert = "Insert";

		// Constructor
		public SysMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		public async Task<BodyResponse<BaseModel>> Insert(SysMenu model)
		{
			// Mengirim model ke API Insert dengan HTTP Post
			var res = await _ifinsysClient.Post(_apiController, _apiRouteInsert, model);
			return res;
		}
	}
}
```

### UpdateByID

```cs
// File: Data/Service/SysMenuService.cs

using Helper.HTTPService;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysMenuService
	{
		// IFINSYS HTTP Client
		public readonly IFINSYSClient _ifinsysClient;

		// API Controller
		public readonly string _apiController = "SysMenu";

		// API Route
		public readonly string _apiRouteUpdateByID = "UpdateByID";

		// Constructor
		public SysMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		public async Task<BodyResponse<object>> UpdateByID(SysMenu model)
		{
			// Mengirim model ke API Update dengan HTTP Put
			var res = await _ifinsysClient.Put(_apiController, _apiRouteUpdateByID, model);
			return res;
		}
	}
}
```

### DeleteByID

```cs
// File: Data/Service/SysMenuService.cs

using Helper.HTTPService;

namespace Data.Service
{
	// ServiceAttribute
	[Service]
	public class SysMenuService
	{
		// IFINSYS HTTP Client
		public readonly IFINSYSClient _ifinsysClient;

		// API Controller
		public readonly string _apiController = "SysMenu";

		// API Route
		public readonly string _apiRouteDeleteByID = "DeleteByID";

		// Constructor
		public SysMenuService(IFINSYSClient ifinsysClient)
		{
			_ifinsysClient = ifinsysClient;
		}

		public async Task<BodyResponse<object>> DeleteByID(string[] id)
		{
			// Mengirim model ke API Update dengan HTTP Delete
			var res = await _ifinsysClient.Delete(_apiController, _apiRouteDeleteByID, id);
			return res;
		}
	}
}
```

## Pages

- File dibuat pada directory `Pages` dengan nama directory menyesuaikan **Menu**
- Penamaan **File** bergantung pada **nama menu** atau **kegunaannya** sebagai contoh:

  - Halaman yang menampilkan **list** SysMenu : MenuList.razor
  - Halaman yang menampilkan **detail** SysMenu : MenuDetail.razor
  - Komponen yang menampilkan **list** Parent SysMenu : ParentMenuList.razor
  - dst.

- Setiap halaman atau komponen memiliki file **_(blazor page)_** dengan ekstensi `.razor` dan **_(blazor class)_** dengan ekstensi `.razor.cs`

  - MenuList.razor (blazor page): Berisikan HTML
  - MenuList.razor.cs (blazor class): Berisikan **_property_** dan **_method_** yang digunakan pada **blazor page**

### Blazor Page

- **_Blazor Page_** bisa berupa halaman ataupun komponen. Perbedaannya terletak pada ada tidaknya routing (`@page 'namaroute'`) pada bagian atas **_blazor page_**

  - Halaman **MenuList**

    ```cs
    @* File : Menu\MenuList.razor *@

    @* Route *@
    @page "/systemsetting/module"

    @*
    * HTML Element
    *@
    ```

  - Komponen **ParentMenuList**

    ```cs
    @* File : Menu\ParentMenuList.razor *@

    @*
    * HTML Element
    *@
    ```

### Blazor Class

- **Blazor Class** harus berupa `partial class` dan **_inherit_** terhadap `IFinancing360_UI.Components.BaseComponent` serta melakukan **_override method_** `OnInitializedAsync()`

  ```cs
  // File : MenuList.razor
  using IFinancing360_UI.Components;

  namespace IFinancing360_SYS_UI.Pages.Menu
  {
  	// Partial class dan Inherit IFinancing360_UI.Components.BaseComponent
  	public partial class MenuList : BaseComponent
  	{
  		protected override async Task OnInitializedAsync()
  		{
  			await base.OnInitializedAsync();
  		}
  		/*
  		* Property dan Method
  		*/
  	}
  }
  ```

- **_Method_** dan **_property_** yang digunakan pada **_blazor page_** menggunakan **_access modifiers_** `public`

  ```cs
  // File : MenuList.razor
  using IFinancing360_UI.Components;

  namespace IFinancing360_SYS_UI.Pages.Menu
  {
  	public partial class MenuList : BaseComponent
  	{
  		protected override async Task OnInitializedAsync()
  		{
  			await base.OnInitializedAsync();
  		}


  		#region Add
  		public void Add()
  		{
  			// Navigasi ke halaman add
  			NavigationManager.NavigateTo("/systemsetting/menu/add");
  		}
  		#endregion

  		/*
  		* Property dan Method Lain
  		*/
  	}
  }
  ```

- Gunakan **_attribute_** `[Inject]` pada **_blazor class_** untuk menggunakan `Service` yang telah dibuat sebelumnya.

  ```cs
  // File : MenuList.razor
  using Data.Service;
  using IFinancing360_UI.Components;

  namespace IFinancing360_SYS_UI.Pages.Menu
  {
  	public partial class MenuList : BaseComponent
  	{
  		// Inject SysMenuService
  		[Inject] SysMenuService SysMenuService { get; set; }

  		protected override async Task OnInitializedAsync()
  		{
  			await base.OnInitializedAsync();
  		}

  		#region LoadData
  		public async Task<List<SysMenu>?> LoadData(string keyword)
  		{
  			// Menggunakan SysMenuService untuk mendapat List Data SysMenu
  			return await SysMenuService.GetRows(keyword, 0, 100);
  		}
  		#endregion
  	}
  }
  ```

### Standar Routing

- Pemberian route dilakukan dengan menambahkan `@page 'url'` pada bagian atas **Razor Page**
  ```html
  <!-- File : Menu\MenuList.razor -->
  @page "/systemsetting/menu"
  ```
- Route sesuai dengan yang terdaftar pada `IFINSYS`
- Contoh menu `Menu` (Child menu dari menu **System Setting**) pada Module `IFINSYS`

  - Halaman List : `'systemsetting/menu'`
  - Halaman Detail (Add) : `'systemsetting/menu/add'`
  - Halaman Detail (Update) : `'systemsetting/menu/{ID}'`

- Jika menu detail memiliki detail lagi maka parameter pertama disesuaikan dengan nama menu sebelumnya dan barulah parameter kedua berupa `'{ID}'`, Contoh kasus halaman `Menu` yang memiliki detail `MenuRole` :

  - Halaman Detail MenuRole (Add) : `'systemsetting/menu/{MenuID}/menurole/add'`
  - Halaman Detail MenuRole (Update) : `'systemsetting/menu/{MenuID}/menurole/{ID}'`

## Contoh Pages

### List Page

#### Blazor Page

```html
<!-- File: Pages/Menu/MenuList.razor  -->
@page "/systemsetting/menu"

<Card>
	<!-- #region Section Title -->
	<RadzenRow
		AlignItems="AlignItems.Center"
		JustifyContent="JustifyContent.Start"
		Gap="16"
		class="section-header"
	>
		<RadzenText TextStyle="TextStyle.DisplayH5">Menu List</RadzenText>
	</RadzenRow>
	<!-- #endregion Section Title -->

	<RadzenStack>
		<!-- #region Toolbar -->
		<RadzenRow Gap="8">
			<RadzenButton ButtonStyle="ButtonStyle.Primary" Text="Add" Click="@Add" />
			<RadzenButton
				ButtonStyle="ButtonStyle.Danger"
				Text="Delete"
				Click="@Delete"
				Disabled="@IsLoading"
			/>
		</RadzenRow>
		<!-- #endregion Toolbar -->

		<!-- #region Filter -->
		<RadzenRow Gap="8">
			<!-- #region Filter Module -->
			<FormFieldButtonLookup Name="ModuleID" Label="Module"
			Target="moduleLookup" @bind-Value="@filter.ModuleID"
			Text="@(filter.ModuleID == null ? "Select Module" : $"Selected Module:
			{filter.Module?.Code}")" />
			<!-- #endregion Filter Module -->

			<!-- #region Filter Parent -->
			<FormFieldButtonLookup Name="ParentMenuID" Label="Parent"
			Target="parentLookup" @bind-Value="@filter.ParentMenuID"
			Text="@(filter.ParentMenuID == null ? "Select Parent" : $"Selected Parent:
			{filter.Parent?.Name}")" />
			<!-- #endregion Filter Parent -->
		</RadzenRow>
		<!-- #endregion Filter -->

		<!-- #region List Data -->
		<DataGrid
			@ref="@dataGrid"
			TItem="SysMenu"
			LoadData="LoadData"
			AllowSelected="true"
		>
			<RadzenDataGridColumn TItem="SysMenu" Property="Name" Title="Name">
				<template Context="row">
					<RadzenLink Path="@("/systemsetting/menu/" + row.ID)" Text="@row.Name"
					/>
				</template>
			</RadzenDataGridColumn>

			<RadzenDataGridColumn
				TItem="SysMenu"
				Property="Module.Name"
				Title="Module Name"
			/>

			<RadzenDataGridColumn
				TItem="SysMenu"
				Property="Parent.Name"
				Title="Parent Name"
			/>

			<RadzenDataGridColumn TItem="SysMenu" Property="IsActive" Title="Active">
				<template Context="row"> @(row.IsActive == 1 ? "YES" : "NO") </template>
			</RadzenDataGridColumn>
		</DataGrid>
		<!-- #endregion List Data -->
	</RadzenStack>
</Card>

<!-- #region Module Lookup -->
<SingleSelectLookup
	@ref="@moduleLookup"
	TItem="SysModule"
	LoadData="LoadModuleLookup"
	Title="Module List"
	Select="async (select) => {
								filter.ModuleID = select.ID;
								filter.Module = select;
								await dataGrid.Reload();
							}"
>
	<RadzenDataGridColumn TItem="SysModule" Property="Code" Title="Code" />
	<RadzenDataGridColumn TItem="SysModule" Property="Name" Title="Name" />
</SingleSelectLookup>
<!-- #endregion Module Lookup -->

<!-- #region Parent Lookup -->
<SingleSelectLookup
	@ref="@parentLookup"
	TItem="SysMenu"
	LoadData="LoadParentLookup"
	Title="Parent List"
	Select="async (select) => {
								filter.ParentMenuID = select.ID;
								filter.Parent = select;
								await dataGrid.Reload();
							}"
>
	<RadzenDataGridColumn TItem="SysMenu" Property="Code" Title="Code" />
	<RadzenDataGridColumn TItem="SysMenu" Property="Name" Title="Name" />
</SingleSelectLookup>
<!-- #endregion Parent Lookup -->
```

#### Blazor Class

```cs
using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SYS_UI.Pages.Menu
{
	public partial class MenuList : BaseComponent
	{
		#region Service
		[Inject] SysMenuService SysMenuService { get; set; }
		[Inject] SysModuleService SysModuleService { get; set; }
		#endregion

		#region Component Field
		DataGrid<SysMenu> dataGrid;
		SingleSelectLookup<SysModule> moduleLookup;
		SingleSelectLookup<SysMenu> parentLookup;
		#endregion

		#region Field
		public readonly SysMenu filter = new()
		{
			ModuleID = "all",
			Module = new SysModule { Code = "ALL" },
			ParentMenuID = "all",
			Parent =
				new SysMenu { Name = "ALL" }
		};
		#endregion

		#region OnInitialized
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		#endregion

		#region LoadData
		public async Task<List<SysMenu>?> LoadData(string keyword)
		{
			return await SysMenuService.GetRows(keyword, 0, 100, ModuleID: filter.ModuleID, ParentMenuID: filter.ParentMenuID);
		}
		#endregion

		#region LoadModuleLookup
		public async Task<List<SysModule>> LoadModuleLookup(string keyword)
		{
			return await SysModuleService.GetRowsForLookupModule(keyword, 0, 100, true);
		}
		#endregion

		#region LoadParentLookup
		public async Task<List<SysMenu>> LoadParentLookup(string keyword)
		{
			return await SysMenuService.GetRowsForLookupParent(keyword, 0, 100, filter.ModuleID, true);
		}
		#endregion

		#region Add
		public void Add()
		{
			NavigationManager.NavigateTo("/systemsetting/menu/add");
		}
		#endregion

		#region Delete
		public async void Delete()
		{
			bool? result = await Confirm();

			if (result == true)
			{
				isLoading = true;
				string[] IDList = dataGrid.selectedData.Select(row => row.ID).ToArray();

				await SysMenuService.Delete(IDList);

				await dataGrid.Reload();
				dataGrid.selectedData.Clear();

				isLoading = false;

				StateHasChanged();
			}
		}
		#endregion
	}
}
```

### Detail Page

`NOTE` :

- Route Endpoint Page Detail untuk insert/add : `/add`
- Route Endpoint Page Detail untuk insert/add : `/{ID}`

#### Blazor Page

```html
@page "/systemsetting/menu/add"
@page "/systemsetting/menu/{ID}"

<Card>
	<!-- #region Section Title -->
	<RadzenRow AlignItems="AlignItems.Center" JustifyContent="JustifyContent.Start" Gap="16" class="section-header">
		<RadzenText TextStyle="TextStyle.DisplayH5">Menu Info</RadzenText>
	</RadzenRow>
	<!-- #endregion Section Title -->

	<!-- #region Form -->
	<RadzenTemplateForm TItem="SysMenu" Data="@row" Submit=@OnSubmit>
		<RadzenStack>
			<!-- #region Toolbar -->
			<RadzenRow Gap="8">
				<RadzenButton ButtonType="ButtonType.Submit" ButtonStyle="ButtonStyle.Primary" Text="Save" />
				@if (ID != null)
				{
					<RadzenButton ButtonStyle=@(row.IsActive == 1 ? ButtonStyle.Danger : ButtonStyle.Success) Text=@(row.IsActive ==
					1 ? "Non Active" : "Active") Click="@ChangeActive" />
				}
				<RadzenButton ButtonStyle="ButtonStyle.Danger" Text="Back" Click="@Back" />
			</RadzenRow>
			<!-- #endregion Toolbar -->

			<!-- #region Form Input -->
			@if (row != null)
			{
				<RadzenRow>
					<!-- #region Code -->
					<FormFieldTextBox Label="Code" Name="Code" @bind-Value="@row.Code" Disabled="true" Max="50" />
					<!-- #endregion -->

					<!-- #region Name -->
					<FormFieldTextBox Label="Name" Name="Name" @bind-Value="@row.Name" Required="true" Max="50" />
					<!-- #endregion -->

					<!-- #region Is Active -->
					<FormFieldSwitch Name="IsActive" Label="Active" @bind-Value="@row.IsActive" Disabled />
					<!-- #endregion -->
				</RadzenRow>

				<RadzenRow>
					<!-- #region Module -->
					<FormFieldButtonLookup Name="ModuleID" Label="Module" Target="moduleLookup" @bind-Value="@row.ModuleID"
						Text="@(row.ModuleID == null ? "Select Module" : $"Selected Module: {row.Module?.Code}")" Required="true" />
					<!-- #endregion -->

					<!-- #region Parent -->
					<FormFieldButtonLookup Name="ParentMenuID" Label="Parent" Target="parentLookup"
						@bind-Value="@row.ParentMenuID"
						Text="@(row.ParentMenuID == null ? "Select Parent" : $"Selected Parent: {row.Parent?.Name}")" />
					<!-- #endregion -->

					<!-- #region Abbreviation -->
					<FormFieldTextBox Label="Abbreviation" Name="Abbreviation" @bind-Value="@row.Abbreviation" Max="50" />
					<!-- #endregion -->
				</RadzenRow>

				<RadzenRow>
					<!-- #region RoutingMenu -->
					<FormFieldTextBox Label="Routing Menu" Name="RoutingMenu" @bind-Value="@row.RoutingMenu" Max="50" />
					<!-- #endregion -->

					<!-- #region URLMenu -->
					<FormFieldTextBox Label="Url Menu" Name="URLMenu" @bind-Value="@row.URLMenu" Required="true" Max="50" />
					<!-- #endregion -->

					<!-- #region CssIcon -->
					<FormFieldTextBox Label="Css Icon" Name="CssIcon" @bind-Value="@row.CssIcon" Max="50" />
					<!-- #endregion -->
				</RadzenRow>

				<RadzenRow>
					<!-- #region Type -->
					<FormFieldRadioButton Label="Type" TValue="string" @bind-Value="row.Type"
						DefaultValue="@(MenuType.Link.ToString())">
						@foreach (MenuType menuType in Enum.GetValues<MenuType>())
						{
							<RadzenRadioButtonListItem Text=@(menuType.ToString()) Value=@(menuType.ToString()) />
						}
					</FormFieldRadioButton>
					<!-- #endregion -->

					<!-- #region OrderKey -->
					<FormFieldNumeric Label="Order" Name="OrderKey" @bind-Value="@row.OrderKey" Required="true" Min="1" />
					<!-- #endregion -->
				</RadzenRow>
			}
			<!-- #endregion Form Input -->
	</RadzenTemplateForm>

	<!-- #region List Detail -->
	@if (ID != null)
	{
		<Divider />
		<RadzenRow AlignItems="AlignItems.Center" JustifyContent="JustifyContent.Start" Gap="16" class="section-header">
			<RadzenText TextStyle="TextStyle.DisplayH5">Role List</RadzenText>
			<RadzenProgressBarCircular ShowValue="false" Mode="ProgressBarMode.Indeterminate"
				Size="ProgressBarCircularSize.Small" Visible="@isLoading" />
		</RadzenRow>

		<RadzenStack>
			<RadzenRow Gap="8">
				<RadzenButton ButtonStyle="ButtonStyle.Primary" Text="Add" Click="@Add" />
				<RadzenButton ButtonStyle="ButtonStyle.Danger" Text="Delete" Click="@Delete"  />
			</RadzenRow>

			<DataGrid @ref=@dataGrid TItem="SysMenuRole" LoadData="LoadData" AllowSelected=true>
				<RadzenDataGridColumn TItem="SysMenuRole" Title="Code">
					<Template Context="row">
						<RadzenLink Path="@($"/systemsetting/menu/{ID}/menurole/{row.ID}")">
							@* <RadzenButton Icon="edit_square" Size="ButtonSize.ExtraSmall" ButtonStyle="ButtonStyle.Primary" /> *@
							@row.Code
						</RadzenLink>
					</Template>
				</RadzenDataGridColumn>
				<RadzenDataGridColumn TItem="SysMenuRole" Property="Name" Title="Name" />
				<RadzenDataGridColumn TItem="SysMenuRole" Property="RoleAccess" Title="Role Access">
					<Template Context="row">
						@(roleAccess.FirstOrDefault(i => i.Code == row.RoleAccess)?.Name)
					</Template>
				</RadzenDataGridColumn>
			</DataGrid>
		</RadzenStack>
	}
	<!-- #endregion -->
</Card>


<!-- #region Module Lookup -->
<SingleSelectLookup @ref="@moduleLookup" TItem="SysModule" LoadData="LoadModuleLookup" Title="Module List" Select="(select) => {
								row.ModuleID = select.ID;
								row.Module = select;
							}">
	<RadzenDataGridColumn TItem="SysModule" Property="Code" Title="Code" />
	<RadzenDataGridColumn TItem="SysModule" Property="Name" Title="Name" />
</SingleSelectLookup>
<!-- #endregion -->

<!-- #region Parent Lookup -->
<SingleSelectLookup @ref="@parentLookup" TItem="SysMenu" LoadData="LoadParentLookup" Title="Parent List" Select="(select) => {
								row.ParentMenuID = select.ID;
								row.Parent = select;
							}">
	<RadzenDataGridColumn TItem="SysMenu" Property="Code" Title="Code" />
	<RadzenDataGridColumn TItem="SysMenu" Property="Name" Title="Name" />
</SingleSelectLookup>
<!-- #endregion -->
```

#### Blazor Class

```cs
using Data.Model;
using Data.Service;
using IFinancing360_UI.Components;
using Microsoft.AspNetCore.Components;

namespace IFinancing360_SYS_UI.Pages.Menu
{
  public partial class MenuDetail : BaseComponent
  {
    #region Parameter
    [Parameter]
    public string? ID { get; set; }
    #endregion

    #region Service
    [Inject] SysMenuService SysMenuService { get; set; } = default!;
    [Inject] SysMenuRoleService SysMenuRoleService { get; set; } = default!;
    [Inject] SysModuleService SysModuleService { get; set; } = default!;
    #endregion

    #region Component Field
    DataGrid<SysMenuRole>? dataGrid;
    SingleSelectLookup<SysModule>? moduleLookup;
    SingleSelectLookup<SysMenu>? parentLookup;
    #endregion

    #region Field
    public SysMenu row = new();
    public readonly RoleAccess[] roleAccess = [
            new RoleAccess { Code = "A", Name = "Access" },
            new RoleAccess { Code = "C", Name = "Create" },
            new RoleAccess { Code = "U", Name = "Update" },
            new RoleAccess { Code = "D", Name = "Delete" }
            ];
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
        row = new SysMenu { IsActive = -1 };
      }
      await base.OnInitializedAsync();
    }
    #endregion

    #region GetRow
    protected async Task GetRow()
    {
      row = await SysMenuService.GetRowByID(ID);
      StateHasChanged();
    }
    #endregion

    #region LoadDetail
    protected async Task<List<SysMenuRole>> LoadDetail(string keyword)
    {
      return await SysMenuRoleService.GetRows(keyword, 0, 100, MenuID: ID);
    }
    #endregion

    #region LoadModuleLookup
    protected async Task<List<SysModule>> LoadModuleLookup(string keyword)
    {
      return await SysModuleService.GetRowsForLookupModule(keyword, 0, 100);
    }
    #endregion

    #region LoadParentLookup
    protected async Task<List<SysMenu>> LoadParentLookup(string keyword)
    {
      return await SysMenuService.GetRowsForLookupParent(keyword, 0, 100, row.ModuleID);
    }
    #endregion

    #region Submit
    public async void OnSubmit()
    {
      isLoading = true;

      #region Insert
      if (ID == null)
      {
        var res = await SysMenuService.Insert(row);

        if (res?.Data != null)
        {
          NavigationManager.NavigateTo($"/systemsetting/menu/{res.Data.ID}");
        }

        isLoading = false;
        StateHasChanged();

      }
      #endregion

      #region Update
      else
      {
        var res = await SysMenuService.UpdateByID(row);

        if (res != null)
        {
          isLoading = false;
        }
        StateHasChanged();
      }
      #endregion
    }
    #endregion

    #region ChangeActive
    public async Task ChangeActive()
    {
        if (ID != null)
        {
            isLoading = true;
            var res = await SysMenuService.ChangeStatus(row);

            if (res != null)
            {
                await GetRow();
                isLoading = false;
            }

            StateHasChanged();
        }
    }
    #endregion

    #region Delete
    public async void Delete()
    {
        bool? result = await Confirm();

        if (result == true)
        {
            isLoading = true;
            string[] IDList = dataGrid.selectedData.Select(row => row.ID).ToArray();

            await SysMenuRoleService.Delete(IDList.ToArray());

            dataGrid.selectedData.Clear();

            await dataGrid.Reload();

            isLoading = false;

            StateHasChanged();
        }
    }
    #region Delete


    #region Back
    public void Back()
    {
        NavigationManager.NavigateTo("/systemsetting/menu");
    }
    #endregion

    #region Add
    public void Add()
    {
        NavigationManager.NavigateTo($"/systemsetting/menu/{ID}/menurole/add");
    }
    #endregion
  }
}
```
