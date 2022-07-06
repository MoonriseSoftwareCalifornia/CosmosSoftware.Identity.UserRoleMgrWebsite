using CosmosSoftware.Identity.UserRoleMgrWebsite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Identity.CosmosDb;
using AspNetCore.Identity.CosmosDb.Containers;
using AspNetCore.Identity.CosmosDb.Extensions;
using AspNetCore.Identity.Services.SendGrid;
using AspNetCore.Identity.Services.SendGrid.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Name of the Cosmos database to use
var cosmosIdentityDbName = builder.Configuration.GetValue<string>("CosmosIdentityDbName");

// If this is set, the Cosmos identity provider will:
// 1. Create the database if it does not already exist.
// 2. Create the required containers if they do not already exist.
// IMPORTANT: Remove this setting if after first run. It will improve startup performance.
var setupCosmosDb = builder.Configuration.GetValue<string>("SetupCosmosDb");

// If the following is set, then create the identity database and required containers.
// You can omit the following, or simplify it as needed.
if (bool.TryParse(setupCosmosDb, out var setup) && setup)
{
    var utils = new ContainerUtilities(connectionString, cosmosIdentityDbName);
    utils.CreateDatabaseAsync(cosmosIdentityDbName).Wait();
    utils.CreateRequiredContainers().Wait();
}

builder.Services.AddDbContext<CosmosIdentityDbContext<IdentityUser>>(options =>
  options.UseCosmos(connectionString: connectionString, databaseName: cosmosIdentityDbName));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCosmosIdentity<CosmosIdentityDbContext<IdentityUser>, IdentityUser, IdentityRole>(
      options => options.SignIn.RequireConfirmedAccount = true // Always a good idea :)
    )
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var sendGridApiKey = builder.Configuration.GetValue<string>("SendGridApiKey");
var sendGridOptions = new SendGridEmailProviderOptions(sendGridApiKey, "eric@moonrise.net");

builder.Services.AddSendGridEmailProvider(sendGridOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
app.MapRazorPages();

app.Run();
