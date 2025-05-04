using TennisCourtBookingApp.Provider;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.Utility;
using PdfSharp.Charting;
using Rotativa.AspNetCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc().AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddProviderServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddSession();
#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var webRootPath = app.Environment.WebRootPath;
var rotativaPath = Path.Combine(webRootPath, "extrafolder", "rotativa");
RotativaConfiguration.Setup(webRootPath, rotativaPath);
app.Run();
