using ArtAuctionblazor;
using ArtAuctionblazor.Services.Auth;
using ArtAuctionblazor.Services.AuthProvider;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Register localStorage
builder.Services.AddBlazoredLocalStorage();

//Services
builder.Services.AddScoped<IAuth, AuthService>();

//Configuration for AuthProvider
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProviderService>();


await builder.Build().RunAsync();
