using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SCCFantasy.ApiServices.Api;
using SCCFantasy.Data.Repositories;
using SCCFantasy.Services;
using SCCFantasy.Web.Services;
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

//Web
builder.Services.AddScoped<IPlayerWebService, PlayerWebService>();
builder.Services.AddScoped<IUserWebService, UserWebService>();

//Services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IUserService, UserService>();

//Api
builder.Services.AddScoped<IGeneralInformationApi, GeneralInformationApi>();
builder.Services.AddScoped<IFixturesApi, FixturesApi>();

//Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<CosmosClient>(sp =>
{
    string cosmosDBEndPointUri; string cosmosDBKey;
    var databaseConfigPath = Environment.CurrentDirectory + "\\databaseConfig.json";

    if (File.Exists(databaseConfigPath))
    {
        var databaseConfigJsonString = File.ReadAllText(databaseConfigPath);
        var databaseConfig = JsonConvert.DeserializeObject<DatabaseConfig>(databaseConfigJsonString);

        cosmosDBEndPointUri = databaseConfig.CosmosDBEndPointUri;
        cosmosDBKey = databaseConfig.CosmosDBKey;
    }
    else
    {
        cosmosDBEndPointUri = Environment.GetEnvironmentVariable("CosmosDBEndPointUri");
        cosmosDBKey = Environment.GetEnvironmentVariable("CosmosDBKey");
    }

    return new CosmosClient(cosmosDBEndPointUri, cosmosDBKey);
});

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

class DatabaseConfig
{
    public string CosmosDBEndPointUri { get; set; }

    public string CosmosDBKey { get; set; }
}