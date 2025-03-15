using ApexCharts;
using Append.Blazor.Printing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using LabManagement.Helpers;
using LabManagement.Components;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Infrastructure.Respository;
using LabManagement.Resposities.Respository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using LabManagement.Components.Infrastructure.IRespository;
using LabManagement.Components.Infrastructure.Respository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();
//builder.Services.AddApexCharts();
builder.Services.AddApexCharts(e =>
{
    e.GlobalOptions = new ApexChartBaseOptions
    {
        Debug = true,
        Theme = new Theme { Palette = PaletteType.Palette6 }
    };
});


builder.Services.AddScoped<HtmlRenderer>();
builder.Services.AddScoped<BlazorRenderer>();

builder.Services.AddDataGridEntityFrameworkAdapter();


builder.Services.AddScoped<IPrintingService, PrintingService>();
builder.Services.AddScoped<ClipboardService>();

builder.Services.AddSingleton<IDapperServices, DapperServices>();
builder.Services.AddScoped<ExportService>();
builder.Services.AddTransient<IUserInfoResposity, UserInfoResposity>();
builder.Services.AddTransient<ICustomerResposity, CustomerResposity>();
builder.Services.AddTransient<IEmployeeResposity, EmployeeResposity>();
builder.Services.AddTransient<IPayCodeResposity, PayCodeResposity>();
builder.Services.AddTransient<IFixedPayCodeResposity, FixedPayCodeResposity>();
builder.Services.AddTransient<IProductResposity, ProductResposity>();
builder.Services.AddTransient<ISalesOrderResposity, SalesOrderResposity>();
builder.Services.AddTransient<IProductionResposity, ProductionResposity>();

builder.Services.AddTransient<IDashboardResposity, DashboardResposity>();
builder.Services.AddTransient<IFileOrderResposity, FileOrderResposity>();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.Cookie.Name = "auth_labmanagement";
        option.LoginPath = "/login";
        option.Cookie.MaxAge = TimeSpan.FromMinutes(20);
        //option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
