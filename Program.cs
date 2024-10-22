using Config;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Radzen;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();

builder.Services.AddService();
builder.Services.AddAPIClient();
builder.Services.AddLogging();
builder.Services.AddAuthScoped();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	Console.WriteLine("Production mode");
	app.UseExceptionHandler("/error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	// Use your custom error page for production
	app.UseExceptionHandler("/error"); // Specify the route to your custom error component
	app.UseHsts(); // Optional: Enable HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection();

app.UseStaticFiles();

if (builder.Environment.IsEnvironment("Local"))
{
	StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
