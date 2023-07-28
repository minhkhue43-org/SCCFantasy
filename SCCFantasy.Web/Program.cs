using Newtonsoft.Json.Serialization;
using SCCFantasy.ApiServices.Api;
using SCCFantasy.Services;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "_SCCFantasyCookie";
});

builder.Services.AddScoped<IPlayerWebService, PlayerWebService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGeneralInformationApi, GeneralInformationApi>();
builder.Services.AddScoped<IFixturesApi, FixturesApi>();

var app = builder.Build();

var syncfusionFileKeyPath = Environment.CurrentDirectory + "\\SyncfusionKey.txt";

if (File.Exists(syncfusionFileKeyPath))
{
    var syncfusionKey = File.ReadAllText(syncfusionFileKeyPath);
    SyncfusionLicenseProvider.RegisterLicense(syncfusionKey);
}
else
{
    SyncfusionLicenseProvider.RegisterLicense(Environment.GetEnvironmentVariable("SYNCFUSION_KEY"));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
