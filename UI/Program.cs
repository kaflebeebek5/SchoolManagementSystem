using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using UI.Configuration;
using UI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(Options =>
               {
                   Options.LoginPath = "/Home/Index";
                   Options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(builder.Configuration["ApplicationData:ApplicationTimeOut"]));
                   Options.SlidingExpiration = true;
                   Options.Cookie.HttpOnly = true;
                   Options.LoginPath = "/Home";
                   Options.LogoutPath = "/Home/Logout";
                   Options.AccessDeniedPath = "/Home/AccessDenied";
               });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host"); // Update the route to match your page location
});

app.Run();
