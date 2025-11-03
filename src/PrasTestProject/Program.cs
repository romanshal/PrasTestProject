using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using PrasTestProject.Data.Contexts;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("NewsDbConnction") ?? throw new InvalidOperationException("Connectiob string 'NewsDbConnction not found.'");
builder.Services.AddDbContext<NewsDbContext>(opt =>
    opt.UseNpgsql());

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.SignIn.RequireConfirmedAccount = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<NewsDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

var supportedCultures = new[] { "ru", "en" };
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    opt.SetDefaultCulture("ru");
    opt.AddSupportedCultures(supportedCultures);
    opt.AddSupportedUICultures(supportedCultures);
    opt.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
