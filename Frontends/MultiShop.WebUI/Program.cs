using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
if (serviceApiSettings == null)
{
    throw new InvalidOperationException("The ServiceApiSettings section is missing in the configuration.");
}

builder.Services.AddMultiShopAuthentication();
builder.Services.AddMultiShopGeneralServices(builder.Configuration);
builder.Services.AddMultiShopIdentityHttpClients(serviceApiSettings);
builder.Services.AddMultiShopAuthenticatedHttpClients(serviceApiSettings);
builder.Services.AddMultiShopApplicationHttpClients(serviceApiSettings);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
