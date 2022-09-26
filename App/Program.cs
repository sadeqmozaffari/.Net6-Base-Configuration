using App.DataLayer;
using App.IocConfig;
using App.IocConfig.Api.Middlewares;
using App.IocConfig.Api.Swagger;
using App.ViewModels.DynamicAccess;
using App.ViewModels.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
SiteSettings SiteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddCustomServices();
builder.Services.AddCustomIdentityServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApiVersioning();
builder.Services.AddSwagger();
builder.Services.AddCustomAuthentication(SiteSettings);
builder.Services.ConfigureWritable<SiteSettings>(builder.Configuration.GetSection("SiteSettings"));


//builder.Services.ConfigureApplicationCookie(options =>
//{
//    // Cookie settings
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(3600);
//    options.LoginPath = "/Admin/Manage/SignIn";
//    options.LogoutPath = "/Admin/Manage/SignOut";
//    options.AccessDeniedPath = "/Admin/Manage/AccessDenied";
//    options.SlidingExpiration = true;
//});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
});
builder.Services.AddMvc();
var app = builder.Build();



// Configure the HTTP request pipeline.
var cachePeriod = app.Environment.IsDevelopment() ? "600" : "605800";
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
{
    appBuilder.UseCustomExceptionHandler();
});
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), appBuilder =>
{
    if (app.Environment.IsDevelopment())
        appBuilder.UseDeveloperExceptionPage();
    else
        appBuilder.UseExceptionHandler("/Home/Error");
});
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CacheFiles")),
//    OnPrepareResponse = ctx =>
//    {
//        ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={cachePeriod}");
//    },
//    RequestPath = "/CacheFiles",
//});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomIdentityServices();

//app.Use(async (context, next) =>
//{
//    await next();
//    if (context.Response.StatusCode == 404)
//    {
//        context.Request.Path = "/home/error404";
//        await next();
//    }
//});

app.UseSwaggerAndUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
//app.UseEndpoints(endpoints =>
//{

//    endpoints.MapControllerRoute(
//          name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//     );

//    endpoints.MapControllerRoute(
//   name: "default",
//   pattern: "{controller=Home}/{action=Index}/{id?}"
//    );

//});
app.Run();
