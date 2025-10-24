using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSCC_WebAppProg_SeatYourself.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NSCC_WebAppProg_SeatYourselfContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NSCC_WebAppProg_SeatYourselfContext") ?? throw new InvalidOperationException("Connection string 'NSCC_WebAppProg_SeatYourselfContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Services to the container.
builder.Services.AddControllersWithViews();

//Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set cookie expiration time
        options.SlidingExpiration = true; // Enable sliding expiration
        options.LoginPath = "/Account/Login"; // Redirect to this path if not authenticated
        options.LogoutPath = "/Account/Logout"; // Redirect to this path on logout
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to this path if access is denied
    });

// Add user secrets functionality
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

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

app.UseAuthentication(); // Enable authentication middleware

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
