using Chapter5Routes;
using Chapter5Routes.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseRouting();
app.UseAntiforgery();

// Example for‚page 3. 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Example for page 4
//app.MapGet("/", () => "Welcome in Chapter 5!");

// Example for page 5
//app.UseEndpoints(static endpoints =>
//  {
//      // Route configurations
//      endpoints.MapGet("/", async context =>
//            {
//                await context.Response.WriteAsync("Welcome to Chapter 5!");
//            });
//      endpoints.MapGet("/about", async context =>
//            {
//                await context.Response.WriteAsync("About chapter 5.");
//            });
//  });

// Example for page 5 and 6
app.UseMinimalApi();

// Example for page 6
app.MapControllerRoute(
    name: "expiration",
    pattern: "expiration/{year}/{month}/{day}/{category}/{slug}",
    defaults: new { controller = "Products", action = "Expiration" });

app.MapControllerRoute(
    name: "products",
    pattern: "products",
    defaults: new { controller = "Products", action = "GetAllProducts" });

// Esample for page 8
app.MapControllerRoute(
    name: "catchAll",
    pattern: "api/{**path}",
    defaults: new { controller = "Error", action = "Handle404" });

app.Run();

