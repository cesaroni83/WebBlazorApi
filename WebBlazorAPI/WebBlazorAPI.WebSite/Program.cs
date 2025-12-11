using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using WebBlazorAPI.WebSite;
using WebBlazorAPI.WebSite.Authentication;
using WebBlazorAPI.WebSite.Repositorio;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7082/") });

builder.Services.AddBlazorBootstrap();
// ?? Aquí puedes agregar la configuración del JSON:
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true; // Ignora mayúsculas/minúsculas en nombres de propiedades
});
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredModal();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
