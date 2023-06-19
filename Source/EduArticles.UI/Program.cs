using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using EduArticles.BusinessLogic;
using EduArticles.BusinessLogic.Interfaces;
using EduArticles.DB;
using EduArticles.DB.Extensions;
using EduArticles.Models.Entities;
using EduArticles.UI;
using EduArticles.UI.Authorization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddDataAccess(builder.Configuration["ConnectionString"]);
builder.Services.AddScoped<IUser, User>();
builder.Services.AddHttpClient();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    const string defaultCulture = "en-GB";
    var supportedCultures = new[]
    {
        new CultureInfo(defaultCulture),
        new CultureInfo("es")
    };

    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.RootDirectory = "/Pages";
        options.Conventions.AddPageRoute(RouteConstants.CommunityView, "r/{name}");
        options.Conventions.AddPageRoute(RouteConstants.CommunityCreate, "new/community");
        options.Conventions.AddPageRoute(RouteConstants.CommunityDelete, "r/{name}/delete");
        options.Conventions.AddPageRoute(RouteConstants.CommunityEdit, "r/{name}/edit");
        options.Conventions.AddPageRoute(RouteConstants.PostView, "p/{id}");
        options.Conventions.AddPageRoute(RouteConstants.PostCreate, "new/post");
        options.Conventions.AddPageRoute(RouteConstants.PostDelete, "p/{id}/delete");
        options.Conventions.AddPageRoute(RouteConstants.PostEdit, "p/{id}/edit");
        options.Conventions.AddPageRoute(RouteConstants.ReplyDelete, "reply/{id}/delete");
        options.Conventions.AddPageRoute(RouteConstants.AccountRegister, "register");
        options.Conventions.AddPageRoute(RouteConstants.AccountLogin, "/login");
        options.Conventions.AddPageRoute(RouteConstants.AccountLogout, "/logout");
        options.Conventions.AddPageRoute(RouteConstants.Search, "/search");
        options.Conventions.AddPageRoute(RouteConstants.Profile, "u/{name}");
    });

builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 5;
    x.Password.RequireLowercase = true;
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

app.InitializeDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(RouteConstants.Error);
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Culture}/{action=SetCulture}");


app.MapRazorPages();

app.Run();
