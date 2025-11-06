using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PrasTestProject.Data.Contexts;
using PrasTestProject.Data.Storages;
using PrasTestProject.Extensions;
using PrasTestProject.Factories;
using PrasTestProject.Interfaces.Factories;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Interfaces.UnitOfWork;
using PrasTestProject.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("NewsDbConnction") ?? throw new InvalidOperationException("Connectiob string 'NewsDbConnction not found.'");
builder.Services.AddDbContext<NewsDbContext>(opt =>
    opt.UseNpgsql(connectionString));

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});

builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services
    .AddScoped<ICreateNewsStorage, CreateNewsStorage>()
    .AddScoped<IEditNewsStorage, EditNewsStorage>()
    .AddScoped<IDeleteNewsStorage, DeleteNewsStorage>()
    .AddScoped<IImageStorage, LocalImageStorage>()
    .AddScoped<INewsDetailsStorage, NewsDetailsStorage>()
    .AddScoped<INewsListStorage, NewsListStorage>();

builder.Services.AddSingleton<IFileNameFactory, FileNameFactory>();
builder.Services.AddSingleton<IFilePathFactory, FilePathFactory>();

builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(opt =>
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

builder.Services
    .AddControllersWithViews()
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

app.MigrateDatabase<NewsDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<DataSeedMaker>>();
    var roleManager = services.GetService<RoleManager<IdentityRole<Guid>>>();
    var userManager = services.GetService<UserManager<IdentityUser<Guid>>>();
    DataSeedMaker
        .SeedAsync(context, logger!, roleManager!, userManager!)
        .Wait();
});

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
