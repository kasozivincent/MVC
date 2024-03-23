using BlazorApp1;
using BlazorApp1.Controllers;
using BlazorApp1.Model;
using BlazorApp1.Model.Repository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<Bank>();
builder.Services.AddScoped<CreationController>();
builder.Services.AddScoped<InformationController>();
builder.Services.AddScoped<AccountsController>();
builder.Services.AddScoped<IBankRepository, BankRepository>();


builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();