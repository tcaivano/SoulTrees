using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Radzen;
using SoulTrees.Areas.Identity;
using SoulTrees.Data;
using SoulTrees.Managers;
using SoulTrees.Repositories;
using SoulTrees.Services;
using System.Net;


var builder = WebApplication.CreateBuilder(args);


string port = builder.Configuration.GetValue<string>("KestrelPort");
builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.MaxConcurrentUpgradedConnections = 100;
    options.Limits.MaxRequestBodySize = 52428800;
    options.Listen(IPAddress.Loopback, 5000);
    options.Listen(IPAddress.Any, int.Parse(port.Remove(0, 1)));
    options.Listen(IPAddress.Any, int.Parse(port.Remove(0, 1)) + 1, listenOptions =>
    {
        listenOptions.UseHttps("certificate.pfx", builder.Configuration.GetValue<string>("KestrelCertPassword"));
    });
})
    .UseContentRoot(Directory.GetCurrentDirectory());

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MariaDb") ?? throw new InvalidOperationException("Connection string 'MariaDb' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));*/

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IItemManager, ItemManager>();
builder.Services.AddTransient<SoulTrees.Repositories.ILogger, Logger>();
builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       builder.Configuration.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}


//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
    var admin = await userManager.FindByEmailAsync(builder.Configuration.GetSection("UserConfig").GetValue<string>("AdminEmail"));
    if (admin != null) await userManager.AddToRoleAsync(admin, "Admin");
}

app.Run();
