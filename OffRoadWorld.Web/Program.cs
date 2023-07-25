using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OffRoadWorld.Data;
using OffRoadWorld.Data.Models;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Services.Data.Services;
using Microsoft.AspNetCore.Identity;
using OffRoadWorld.Web.Modelbinders;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLServer");

builder.Services.AddDbContext<OffRoadWorldDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IMarketplaceService, MarketplaceService>();

builder.Services.AddHttpClient();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<OffRoadWorldDbContext>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "bg" };

    options.SetDefaultCulture("en-US")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

var app = builder.Build();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions!.Value);

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/News/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "forum",
        pattern: "Forum/Topic/{categoryId}",
        defaults: new { controller = "Forum", action = "Topic" });

    endpoints.MapRazorPages();
});

app.Run();