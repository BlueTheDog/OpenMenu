using BlazorAdminUI;
using BlazorAdminUI.MessageHandler;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7294/api/") });
//builder.Services.AddHttpClient("companiesAPI", cl =>
//{
//    cl.BaseAddress = new Uri("https://localhost:5001/api/");
//})
//.AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

//builder.Services.AddHttpClient("companyAPI.Unauthorized", client =>
//    client.BaseAddress = new Uri("https://localhost:5001/api/"));

//builder.Services.AddScoped(
//    sp => sp.GetService<IHttpClientFactory>().CreateClient("companiesAPI"));



builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    //builder.Configuration.Bind("Local", options.ProviderOptions);
    builder.Configuration.Bind("oidc", options.ProviderOptions);
});

await builder.Build().RunAsync();
