using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Data;
using StoEtDash.Web.Database.Services;

var builder = WebApplication.CreateBuilder(args);

// Setup session so we can store data there
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// Add razor runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add ToastNotification
builder.Services.AddNotyf(config =>
{
	config.DurationInSeconds = 3;
	config.IsDismissable = true;
	config.Position = NotyfPosition.TopRight;
	config.HasRippleEffect = true;
});

// Add DatabaseService
var userRepository = new UserRepository();
var transactionRepository = new TransactionRepository();
var marketRepositoryApi = new MarketRepositoryApi("E87EC8AJVXXRIZZ4");
var currencyExchangeRateRepositoryApi = new CurrencyExchangeRateRepositoryApi();

var databaseService = new DatabaseService(userRepository, transactionRepository, marketRepositoryApi, currencyExchangeRateRepositoryApi);
builder.Services.AddSingleton<IDatabaseService>(databaseService);

var app = builder.Build();

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

app.UseNotyf();

app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
