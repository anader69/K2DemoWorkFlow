using Commons.K2.Proxy;
using Framework.Core.SharedServices.Services;
using K2DemoWorkFlow;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationServices(connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles(); 
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;
 
app.Run();
